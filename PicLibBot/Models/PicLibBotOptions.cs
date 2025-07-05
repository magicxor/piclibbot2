using System.ComponentModel.DataAnnotations;

namespace PicLibBot.Models;

internal sealed class PicLibBotOptions
{
    [Required]
    [RegularExpression(".*:.*")]
    public required string TelegramBotApiKey { get; init; }

    [Required]
    [MinLength(1)]
    public required string BraveApiKey { get; init; }

    [Required]
    [Range(3, 50)]
    public required int MaxInlineResults { get; init; }
}
