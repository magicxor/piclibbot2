using System.Text.Json.Serialization;

namespace PicLibBot.Models.Brave;

/// <summary>
/// Aggregated information about a URL.
/// </summary>
public sealed class MetaUrl
{
    [JsonPropertyName("scheme")]
    public string? Scheme { get; set; }

    [JsonPropertyName("netloc")]
    public string? Netloc { get; set; }

    [JsonPropertyName("hostname")]
    public string? Hostname { get; set; }

    [JsonPropertyName("favicon")]
    public string? Favicon { get; set; }

    [JsonPropertyName("path")]
    public string? Path { get; set; }
}
