using System.Text.Json.Serialization;

namespace PicLibBot.Models.Brave;

/// <summary>
/// A model representing an image result for the requested query.
/// </summary>
public sealed class ImageResult
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("source")]
    public string? Source { get; set; }

    [JsonPropertyName("page_fetched")]
    public string? PageFetched { get; set; }

    [JsonPropertyName("thumbnail")]
    public Thumbnail? Thumbnail { get; set; }

    [JsonPropertyName("properties")]
    public Properties? Properties { get; set; }

    [JsonPropertyName("meta_url")]
    public MetaUrl? MetaUrl { get; set; }

    [JsonPropertyName("confidence")]
    public string? Confidence { get; set; }
}
