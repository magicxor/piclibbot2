using System.Text.Json.Serialization;

namespace PicLibBot.Models.Brave;

/// <summary>
/// Additional information about the image search results.
/// </summary>
public sealed class Extra
{
    [JsonPropertyName("might_be_offensive")]
    public bool? MightBeOffensive { get; set; }
}
