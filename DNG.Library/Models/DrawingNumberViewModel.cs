namespace DNG.Library.Models
{
    public class DrawingNumberViewModel
    {
        public bool IncludeBeltSeries { get; set; } = true;
        public bool IncludeBeltType { get; set; } = true;
        public bool ShowQueryString { get; set; } = false;
        public bool IncludeColor { get; set; } = false;
        public bool IncludeMaterial { get; set; } = false;
        public bool IncludeAdderMaterial { get; set; } = false;
        public bool IncludeRodMaterial { get; set; } = false;
        public bool IncludeBeltWidth { get; set; } = true;
        public bool IncludeQtyRollersAcrossWidth { get; set; } = false;
        public bool IncludeFRGCenters { get; set; } = true;
        public bool IncludeBeltAccessories { get; set; } = false;
        public bool IncludeFrictionAntiStatic { get; set; } = false;
        public bool IncludeFlightsRollersGrips { get; set; } = true;
        public bool IncludeSidePLLaneDV { get; set; } = false;
        public bool IncludeUniqueIdentification { get; set; } = false;
        public bool IncludeIndentCode { get; set; } = true;
    }
}