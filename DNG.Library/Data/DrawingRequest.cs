using System;
using System.ComponentModel.DataAnnotations;

namespace DNG.Library.Data
{
    public class DrawingRequest : IDrawingRequest
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string BeltType { get; set; } = string.Empty;
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
        public string UniqueIdentification { get; set; } = "5";
        public string IndentCode { get; set; } = string.Empty;
        public string SalesOrderNumber { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public string AssignedTo { get; set; } = string.Empty;
        public string QueryString { get; set; } = string.Empty;

        public Dictionary<string, string> SpecialCaseInfo { get; set; } = new Dictionary<string, string>();
        public string QuoteNumber { get; set; } = string.Empty;
        public int NumberOfLinks { get; set; }
        public int BeltLengthLinearFeet { get; set; }
        public int BeltLengthSquareFeet { get; set; }

        public string[] GetPropertyValues()
        {
            return new string[]
            {
                BeltType,
                BeltSeries,
                Color,
                Material,
                AdderMaterial,
                RodMaterial,
                BeltWidth.ToString(),
                FlightsRollersGrip,
                QtyRollersAcrossWidth.ToString(),
                FRGCenters.ToString(),
                BeltAccessories,
                FrictionAntiStatic,
                SidePLLaneDV,
                UniqueIdentification,
                IndentCode
            };
        }
    }
}