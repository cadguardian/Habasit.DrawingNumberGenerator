namespace DNG.Library.Models
{
    public class DrawingNumberViewModel
    {
        public bool IncludeBeltSeries { get; set; } = true;
        public bool IncludeBeltType { get; set; } = true;
        public bool ShowQueryString { get; set; } = false;
        public bool IncludeColor { get; set; } = true;
        public bool IncludeMaterial { get; set; } = true;
        public bool IncludeAdderMaterial { get; set; } = true;
        public bool IncludeRodMaterial { get; set; } = true;
        public bool IncludeBeltWidth { get; set; } = true;
        public bool IncludeQtyRollersAcrossWidth { get; set; } = true;
        public bool IncludeFRGCenters { get; set; } = true;
        public bool IncludeBeltAccessories { get; set; } = true;
        public bool IncludeFrictionAntiStatic { get; set; } = true;
        public bool IncludeFlightsRollersGrips { get; set; } = true;
        public bool IncludeSidePLLaneDV { get; set; } = true;
        public bool IncludeUniqueIdentification { get; set; } = true;
        public bool IncludeIndentCode { get; set; } = true;
    }
}