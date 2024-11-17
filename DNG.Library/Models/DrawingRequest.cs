using System.ComponentModel.DataAnnotations;
using DNG.Library.Models.Base;

namespace DNG.Library.Models
{
    public class DrawingRequest : IDrawingRequest
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(10)]
        public string PurchaseOrderNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Belt Type is required.")]
        public string BeltType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Belt Series is required.")]
        public string BeltSeries { get; set; } = string.Empty;

        [Required(ErrorMessage = "Belt Color is required.")]
        public string Color { get; set; } = string.Empty;

        [Required(ErrorMessage = "Belt Material is required.")]
        public string Material { get; set; } = string.Empty;

        public string AdderMaterial { get; set; } = string.Empty;

        [Required(ErrorMessage = "Rod Material is required.")]
        public string RodMaterial { get; set; } = string.Empty;

        [Required(ErrorMessage = "Belt Width is required.")]
        public decimal BeltWidth { get; set; }

        public string ThumbnailImageFilePath { get; set; } = string.Empty;

        public int QtyRollersAcrossWidth { get; set; }
        public decimal FRGCenters { get; set; }
        public string FlightsRollersGrip { get; set; } = string.Empty;
        public string BeltAccessories { get; set; } = string.Empty;
        public string FrictionAntiStatic { get; set; } = string.Empty;
        public string SidePLLaneDV { get; set; } = string.Empty;

        [Required(ErrorMessage = "Uniques ID is required.")]
        public string UniqueIdentification { get; set; } = "5";

        public string IndentCode { get; set; } = string.Empty;

        [StringLength(10)]
        public string SalesOrderNumber { get; set; } = string.Empty;

        public DateTime StartDate { get; set; } = DateTime.Now;
        public string AssignedTo { get; set; } = string.Empty;
        public string QueryString { get; set; } = string.Empty;
        public List<KeyValuePair<string, string>> PartsList { get; set; } = new List<KeyValuePair<string, string>>();

        public Dictionary<string, string> SpecialCaseInfo { get; set; } = new Dictionary<string, string>();

        [StringLength(10)]
        public string QuoteNumber { get; set; } = string.Empty;

        public string SugarCRMTaskLink { get; set; } = string.Empty;
        public string FinalDrawingFilePath { get; set; } = string.Empty;
        public int NumberOfLinks { get; set; }
        public int BeltLengthLinearFeet { get; set; }
        public int BeltLengthSquareFeet { get; set; }

        [Required(ErrorMessage = "Structured Text is required.")]
        public string StructuredText { get; set; } = string.Empty;

        public string CadTemplatePath { get; set; } = string.Empty;
        public string ReferenceDrawingPath { get; set; } = string.Empty;

        public string GetJobNumber() => !string.IsNullOrWhiteSpace(SalesOrderNumber) ? $"SO{SalesOrderNumber}" :
                                      !string.IsNullOrWhiteSpace(PurchaseOrderNumber) ? $"PO{PurchaseOrderNumber}" :
                                      !string.IsNullOrWhiteSpace(QuoteNumber) ? $"Q{QuoteNumber}" : "";

        public string GetProjectFolderName()
        {
            var jobNumber = GetJobNumber();
            var beltSeries = BeltSeries;

            if (string.IsNullOrWhiteSpace(jobNumber) && string.IsNullOrWhiteSpace(beltSeries))
            {
                return string.Empty; // Return nothing if both are empty
            }

            // Show " - " only if both are non-empty
            return !string.IsNullOrWhiteSpace(jobNumber) && !string.IsNullOrWhiteSpace(beltSeries)
                ? $"{jobNumber} - {beltSeries}"
                : $"{jobNumber}{beltSeries}";
        }

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