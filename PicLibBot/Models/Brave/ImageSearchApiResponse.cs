using System.Text.Json.Serialization;

namespace PicLibBot.Models.Brave;

/// <summary>
/// Top level response model for successful Image Search API requests.
/// </summary>
public sealed class ImageSearchApiResponse
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("query")]
    public Query? Query { get; set; }

    [JsonPropertyName("results")]
    public List<ImageResult>? Results { get; set; }

    [JsonPropertyName("extra")]
    public Extra? Extra { get; set; }
}
