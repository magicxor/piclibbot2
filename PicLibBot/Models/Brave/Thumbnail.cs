using System.Text.Json.Serialization;

namespace PicLibBot.Models.Brave;

/// <summary>
/// Aggregated details representing the image thumbnail.
/// </summary>
public sealed class Thumbnail
{
    [JsonPropertyName("src")]
    public string? Src { get; set; }

    [JsonPropertyName("width")]
    public int? Width { get; set; }

    [JsonPropertyName("height")]
    public int? Height { get; set; }
}
