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