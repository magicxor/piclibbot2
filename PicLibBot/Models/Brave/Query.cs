using System.Text.Json.Serialization;

namespace PicLibBot.Models.Brave;

/// <summary>
/// A model representing information gathered around the requested query.
/// </summary>
public sealed class Query
{
    [JsonPropertyName("original")]
    public string? Original { get; set; }

    [JsonPropertyName("altered")]
    public string? Altered { get; set; }

    [JsonPropertyName("spellcheck_off")]
    public bool? SpellcheckOff { get; set; }

    [JsonPropertyName("show_strict_warning")]
    public bool? ShowStrictWarning { get; set; }
}
