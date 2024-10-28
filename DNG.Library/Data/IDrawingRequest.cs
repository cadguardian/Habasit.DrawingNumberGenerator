namespace DNG.Library.Data
{
    public interface IDrawingRequest
    {
        string AdderMaterial { get; set; }
        string QueryString { get; set; }
        string AssignedTo { get; set; }
        string BeltAccessories { get; set; }
        string BeltSeries { get; set; }
        string BeltType { get; set; }
        decimal BeltWidth { get; set; }
        string Color { get; set; }
        string FlightsRollersGrip { get; set; }
        decimal FRGCenters { get; set; }
        string FrictionAntiStatic { get; set; }
        Guid Id { get; set; }
        string IndentCode { get; set; }
        string Material { get; set; }
        int QtyRollersAcrossWidth { get; set; }
        string RodMaterial { get; set; }
        string SalesOrderNumber { get; set; }
        string SidePLLaneDV { get; set; }
        Dictionary<string, string> SpecialCaseInfo { get; set; }
        DateTime StartDate { get; set; }
        string UniqueIdentification { get; set; }

        string[] GetPropertyValues();
    }
}