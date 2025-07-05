using System.Text.Json.Serialization;

namespace PicLibBot.Models.Brave;

/// <summary>
/// Metadata on an image.
/// </summary>
public sealed class Properties
{
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("placeholder")]
    public string? Placeholder { get; set; }

    [JsonPropertyName("width")]
    public int? Width { get; set; }

    [JsonPropertyName("height")]
    public int? Height { get; set; }
}
