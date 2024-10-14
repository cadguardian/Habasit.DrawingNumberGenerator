using System.ComponentModel.DataAnnotations;

namespace DNG.Library.Data;

public class DrawingNumberInput
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string BeltType { get; set; } = string.Empty;

    [Required]
    public string BeltSeries { get; set; } = string.Empty;

    [Required]
    public string Color { get; set; } = string.Empty;

    [Required]
    public string Material { get; set; } = string.Empty;

    public string AdderMaterial { get; set; } = string.Empty;

    [Required]
    public string RodMaterial { get; set; } = string.Empty;

    [Range(1, 999.9), Required]
    public decimal BeltWidth { get; set; }

    public int QtyRollersAcrossWidth { get; set; }

    public decimal FRGCenters { get; set; }

    public string FlightsRollersGrip { get; set; } = string.Empty;
    public string BeltAccessories { get; set; } = string.Empty;
    public string FrictionAntiStatic { get; set; } = string.Empty;
    public string SidePLLaneDV { get; set; } = string.Empty;

    [StringLength(2), Required]
    public string UniqueIdentification { get; set; } = string.Empty;

    [Required]
    public string IndentCode { get; set; } = string.Empty;
}