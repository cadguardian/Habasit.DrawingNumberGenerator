using System;
using System.ComponentModel.DataAnnotations;

namespace DNG.Library.Models
{
    public class DrawingNumber : IDrawingNumber
    {
        [Key]
        public static Guid Id => Guid.NewGuid();

        [StringLength(1, ErrorMessage = "Belt Type code must be 1 character.")]
        public string BeltTypeCode { get; set; } = string.Empty;

        [StringLength(8, ErrorMessage = "Belt Series code can be at most 8 characters.")]
        public string BeltSeriesCode { get; set; } = string.Empty;

        [StringLength(1, ErrorMessage = "Color code must be 1 character.")]
        public string ColorCode { get; set; } = string.Empty;

        [StringLength(1, ErrorMessage = "Material code must be 1 character.")]
        public string MaterialCode { get; set; } = string.Empty;

        [StringLength(1, ErrorMessage = "Adder Material code must be 1 character.")]
        public string AdderMaterialCode { get; set; } = string.Empty;

        [StringLength(1, ErrorMessage = "Rod Material code must be 1 character.")]
        public string RodMaterialCode { get; set; } = string.Empty;

        [StringLength(4, ErrorMessage = "Belt Width code must be 4 characters.")]
        public string BeltWidthCode { get; set; } = string.Empty;

        [Range(0, 99, ErrorMessage = "Qty Rollers Across Width must be between 0 and 99.")]
        public string QtyRollersAcrossWidth { get; set; } = string.Empty;

        [StringLength(3, ErrorMessage = "F/R/G Centers must be 3 characters.")]
        public string FRGCenters { get; set; } = string.Empty;

        [StringLength(2, ErrorMessage = "Belt Accessories code must be 2 characters.")]
        public string BeltAccessoriesCode { get; set; } = string.Empty;

        [StringLength(1, ErrorMessage = "Friction/Anti Static code must be 1 character.")]
        public string FrictionAntiStaticCode { get; set; } = string.Empty;

        [StringLength(1, ErrorMessage = "Flights/Rollers/Grips code can be at most 1 character.")]
        public string FlightsRollersGripsCode { get; set; } = string.Empty;

        [StringLength(1, ErrorMessage = "Side-PL/Lane-DV code must be 1 character.")]
        public string SidePLLaneDVCode { get; set; } = string.Empty;

        // Unique Identification and Indent Code (1 and 2 characters respectively)
        [StringLength(1, ErrorMessage = "Unique Identification must be up to 1 character.")]
        public string UniqueIdentification { get; set; } = string.Empty;

        [StringLength(2, ErrorMessage = "Indent Code must be up to 2 characters.")]
        public string IndentCode { get; set; } = string.Empty;

        public string QueryString { get; set; } = string.Empty;
        public string DrawingCode { get; set; } = string.Empty;

        //Todo break query string out to its own object

        // Toggle switches for each field in the query string
        public bool IncludeBeltType { get; set; } = true;

        public bool IncludeBeltSeries { get; set; } = true;
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

        public static DrawingNumber Create()
        {
            return new DrawingNumber();
        }

        private string _drawingCode = string.Empty;

        public string GetDrawingQueryString()
        {
            var values = new List<string>();

            if (IncludeBeltType) values.Add(BeltTypeCode);
            if (IncludeBeltSeries) values.Add(BeltSeriesCode);
            if (IncludeColor) values.Add(ColorCode);
            if (IncludeMaterial) values.Add(MaterialCode);
            if (IncludeAdderMaterial) values.Add(AdderMaterialCode);
            if (IncludeRodMaterial) values.Add(RodMaterialCode);
            if (IncludeBeltWidth) values.Add(BeltWidthCode);
            if (IncludeQtyRollersAcrossWidth) values.Add(QtyRollersAcrossWidth);
            if (IncludeFRGCenters) values.Add(FRGCenters);
            if (IncludeBeltAccessories) values.Add(BeltAccessoriesCode);
            if (IncludeFrictionAntiStatic) values.Add(FrictionAntiStaticCode);
            if (IncludeFlightsRollersGrips) values.Add(FlightsRollersGripsCode);
            if (IncludeSidePLLaneDV) values.Add(SidePLLaneDVCode);
            if (IncludeUniqueIdentification) values.Add(UniqueIdentification);
            if (IncludeIndentCode) values.Add(IndentCode);

            QueryString = string.Join("*", values.Select(value => value.Replace("-", "")));
            return QueryString;
        }

        public string GetDrawingNumber()
        {
            // Retrieve property values and concatenate them directly
            DrawingCode = string.Join(string.Empty, GetPropertyValues());

            return DrawingCode;
        }

        public string GetDrawingNumberLog()
        {
            _drawingCode =
                $"Belt Type: {BeltTypeCode}\n" +
                $"Belt Series: {BeltSeriesCode}\n" +
                $"Color: {ColorCode}\n" +
                $"Material: {MaterialCode}\n" +
                $"Adder Material: {AdderMaterialCode}\n" +
                $"Rod Material: {RodMaterialCode}\n" +
                $"Belt Width: {BeltWidthCode}\n" +
                $"Flights/Rollers/Grip: {FlightsRollersGripsCode}\n" +
                $"Qty. Rollers Across Width: {QtyRollersAcrossWidth}\n" +
                $"F/R/G Centers (inches): {FRGCenters}\n" +
                $"Belt Accessories: {BeltAccessoriesCode}\n" +
                $"Friction/Anti-Static: {FrictionAntiStaticCode}\n" +
                $"Side-PL/Lane-DV: {SidePLLaneDVCode}\n" +
                $"Unique Identification: {UniqueIdentification}\n" +
                $"Indent Code: {IndentCode}";
            return _drawingCode;
        }

        // New method to return property values in specified order
        public string[] GetPropertyValues()
        {
            return new string[]
            {
                BeltTypeCode,
                BeltSeriesCode,
                ColorCode,
                MaterialCode,
                AdderMaterialCode,
                RodMaterialCode,
                BeltWidthCode,
                FlightsRollersGripsCode,
                QtyRollersAcrossWidth,
                FRGCenters,
                BeltAccessoriesCode,
                FrictionAntiStaticCode,
                SidePLLaneDVCode,
                UniqueIdentification,
                IndentCode
            };
        }
    }
}