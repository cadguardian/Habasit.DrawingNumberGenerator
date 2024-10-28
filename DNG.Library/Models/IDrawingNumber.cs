namespace DNG.Library.Models
{
    public interface IDrawingNumber
    {
        string AdderMaterialCode { get; set; }
        string BeltAccessoriesCode { get; set; }
        string BeltSeriesCode { get; set; }
        string BeltTypeCode { get; set; }
        string BeltWidthCode { get; set; }
        string ColorCode { get; set; }
        string DrawingCode { get; set; }
        string FlightsRollersGripsCode { get; set; }
        string FRGCenters { get; set; }
        string FrictionAntiStaticCode { get; set; }
        bool IncludeAdderMaterial { get; set; }
        bool IncludeBeltAccessories { get; set; }
        bool IncludeBeltSeries { get; set; }
        bool IncludeBeltType { get; set; }
        bool IncludeBeltWidth { get; set; }
        bool IncludeColor { get; set; }
        bool IncludeFlightsRollersGrips { get; set; }
        bool IncludeFRGCenters { get; set; }
        bool IncludeFrictionAntiStatic { get; set; }
        bool IncludeIndentCode { get; set; }
        bool IncludeMaterial { get; set; }
        bool IncludeQtyRollersAcrossWidth { get; set; }
        bool IncludeRodMaterial { get; set; }
        bool IncludeSidePLLaneDV { get; set; }
        bool IncludeUniqueIdentification { get; set; }
        string IndentCode { get; set; }
        string MaterialCode { get; set; }
        string QtyRollersAcrossWidth { get; set; }
        string QueryString { get; set; }
        string RodMaterialCode { get; set; }
        string SidePLLaneDVCode { get; set; }
        string UniqueIdentification { get; set; }

        string GetDrawingNumber();
        string GetDrawingNumberLog();
        string GetDrawingQueryString();
        string[] GetPropertyValues();
    }
}