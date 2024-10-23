using System;
using System.ComponentModel.DataAnnotations;

namespace DNG.Library.Data;

public class DrawingRequest : IDrawingRequest
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string BeltType { get; set; } = string.Empty;
    public string QueryString { get; set; } = string.Empty;
    public string BeltSeries { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Material { get; set; } = string.Empty;
    public string AdderMaterial { get; set; } = string.Empty;
    public string RodMaterial { get; set; } = string.Empty;
    public decimal BeltWidth { get; set; }
    public int QtyRollersAcrossWidth { get; set; }
    public decimal FRGCenters { get; set; }
    public string FlightsRollersGrip { get; set; } = string.Empty;
    public string BeltAccessories { get; set; } = string.Empty;
    public string FrictionAntiStatic { get; set; } = string.Empty;
    public string SidePLLaneDV { get; set; } = string.Empty;
    public string UniqueIdentification { get; set; } = string.Empty;
    public string IndentCode { get; set; } = string.Empty;
    public string SalesOrderNumber { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.Now;
    public string AssignedTo { get; set; } = string.Empty;

    public Dictionary<string, string> SpecialCaseInfo { get; set; } = new Dictionary<string, string>();
}