namespace DNG.Library.Models.Base
{
    public interface IDrawingRequest
    {
        string GetProjectFolderName();

        string GetJobNumber();

        string SugarCRMTaskLink { get; set; }
        string ThumbnailImageFilePath { get; set; }
        string FinalDrawingFilePath { get; set; }
        string AdderMaterial { get; set; }
        string CadTemplatePath { get; set; }
        string ReferenceDrawingPath { get; set; }
        string PurchaseOrderNumber { get; set; }
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
        int NumberOfLinks { get; set; }
        int QtyRollersAcrossWidth { get; set; }
        string QueryString { get; set; }
        string QuoteNumber { get; set; }
        string RodMaterial { get; set; }
        string SalesOrderNumber { get; set; }
        string SidePLLaneDV { get; set; }
        Dictionary<string, string> SpecialCaseInfo { get; set; }
        DateTime StartDate { get; set; }
        string UniqueIdentification { get; set; }

        string[] GetPropertyValues();

        string StructuredText { get; set; }
        List<KeyValuePair<string, string>> PartsList { get; set; }
    }
}