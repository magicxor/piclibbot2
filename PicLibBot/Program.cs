using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using PicLibBot.Abstractions;
using PicLibBot.Enums;
using PicLibBot.Exceptions;
using PicLibBot.Extensions;
using PicLibBot.Models;
using PicLibBot.Services;
using Refit;
using Telegram.Bot;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace PicLibBot;

internal static class Program
{
    private const string BraveSearchApiBaseUrl = "https://api.search.brave.com";
    private static readonly LoggingConfiguration LoggingConfiguration = new XmlLoggingConfiguration("nlog.config");

    [SuppressMessage("Major Code Smell", "S2139:Exceptions should be either logged or rethrown but not both", Justification = "Entry point")]
    public static void Main(string[] args)
    {
        // NLog: setup the logger first to catch all errors
        LogManager.Configuration = LoggingConfiguration;
        try
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((_, config) =>
                {
                    config
                        .AddEnvironmentVariables("PICLIBBOT_")
                        .AddJsonFile("appsettings.json", optional: true);
                })
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                    loggingBuilder.AddNLog(LoggingConfiguration);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddOptions<PicLibBotOptions>()
                        .Bind(hostContext.Configuration.GetSection(nameof(OptionSections.PicLibBot)))
                        .ValidateDataAnnotations()
                        .ValidateOnStart();

                    var telegramRequestTimeout = TimeSpan.FromSeconds(9);
                    services.AddHttpClient(nameof(HttpClientType.Telegram), httpClient => httpClient.Timeout = telegramRequestTimeout)
                        .AddDefaultLogger()
                        .AddStandardResilienceHandler(x =>
                        {
                            x.AttemptTimeout = new HttpTimeoutStrategyOptions { Timeout = telegramRequestTimeout };
                            x.TotalRequestTimeout = new HttpTimeoutStrategyOptions { Timeout = x.AttemptTimeout.Timeout * 2 };
                            x.CircuitBreaker.SamplingDuration = x.AttemptTimeout.Timeout * 2;
                        });

                    var braveRequestTimeout = TimeSpan.FromSeconds(9);
                    services.AddRefitClient<IBraveSearch>()
                        .ConfigureHttpClient(c =>
                        {
                            c.BaseAddress = new Uri(BraveSearchApiBaseUrl);
                            c.Timeout = braveRequestTimeout;
                        })
                        .AddDefaultLogger()
                        .AddStandardResilienceHandler(x =>
                        {
                            x.AttemptTimeout = new HttpTimeoutStrategyOptions { Timeout = braveRequestTimeout };
                            x.TotalRequestTimeout = new HttpTimeoutStrategyOptions { Timeout = x.AttemptTimeout.Timeout * 2 };
                            x.CircuitBreaker.SamplingDuration = x.AttemptTimeout.Timeout * 2;
                        });

                    var telegramBotApiKey = hostContext.Configuration.GetTelegramBotApiKey()
                                            ?? throw new ServiceException("Telegram bot API key is missing");
                    services.AddScoped<ITelegramBotClient, TelegramBotClient>(s => new TelegramBotClient(telegramBotApiKey,
                        s.GetRequiredService<IHttpClientFactory>()
                            .CreateClient(nameof(HttpClientType.Telegram))));

                    services.AddScoped<TelegramBotService>();
                    services.AddHostedService<Worker>();
                })
                .Build();

            host.Run();
        }
        catch (Exception ex)
        {
            // NLog: catch setup errors
            LogManager.GetCurrentClassLogger().Error(ex, "Stopped program because of exception");
            throw;
        }
        finally
        {
            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            LogManager.Shutdown();
        }
    }
}
