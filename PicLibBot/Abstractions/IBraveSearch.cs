using PicLibBot.Models.Brave;
using Refit;

namespace PicLibBot.Abstractions;

internal interface IBraveSearch
{
    [Get("/res/v1/images/search?q={query}&safesearch=off&count={count}")]
    Task<ImageSearchApiResponse?> ListImagesAsync(string query, int count, [Header("X-Subscription-Token")] string token, CancellationToken cancellationToken);
}
