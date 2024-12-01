using System.ComponentModel.DataAnnotations;

namespace DNG.Library.Models;

/// <summary>
/// Represents a part number with its description.
/// </summary>
public class PartNumber
{
    [Key]
    public string Part { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}