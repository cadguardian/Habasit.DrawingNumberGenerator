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

        /// <summary>
        /// Returns the name of the output text file based on JobNumber, BeltType, and BeltSeries.
        /// Example: "SO123456 M1220 properties.txt"
        /// </summary>
        public string GetExportFileName()
        {
            // If GetJobNumber() is empty, we use "UNDEFINED"
            string jobNumber = string.IsNullOrWhiteSpace(GetJobNumber()) ? "UNDEFINED" : GetJobNumber();

            // BeltType or BeltSeries can be empty => "UNDEFINED"
            string beltType = string.IsNullOrWhiteSpace(BeltType) ? "UNDEFINED" : BeltType;
            string beltSeries = string.IsNullOrWhiteSpace(BeltSeries) ? "UNDEFINED" : BeltSeries;

            return $"{jobNumber} {beltType}{beltSeries} properties.txt";
        }

        /// <summary>
        /// Builds a multi-line text string where each line is "Property=Value".
        /// If a string property is empty, uses "UNDEFINED".
        /// </summary>
        public string ExportPropertiesToText()
        {
            // Local helper to handle empty strings
            Func<string, string> StrOrUndefined = s =>
                string.IsNullOrWhiteSpace(s) ? "UNDEFINED" : s;

            // For the PartsList
            string partsListValue = PartsList?.Count > 0
                ? string.Join(";", PartsList.Select(kvp => $"{kvp.Key}={kvp.Value}"))
                : "UNDEFINED";

            // For the SpecialCaseInfo dictionary
            string specialCaseValue = SpecialCaseInfo?.Count > 0
                ? string.Join(";", SpecialCaseInfo.Select(kvp => $"{kvp.Key}={kvp.Value}"))
                : "UNDEFINED";

            // Build each line.
            // Note: Numeric/date properties are simply converted to string.
            // Adjust if you need "UNDEFINED" for zero or default values.
            var lines = new List<string>
            {
                $"Id={Id}",
                $"PurchaseOrderNumber={StrOrUndefined(PurchaseOrderNumber)}",
                $"BeltType={StrOrUndefined(BeltType)}",
                $"BeltSeries={StrOrUndefined(BeltSeries)}",
                $"Color={StrOrUndefined(Color)}",
                $"Material={StrOrUndefined(Material)}",
                $"AdderMaterial={StrOrUndefined(AdderMaterial)}",
                $"RodMaterial={StrOrUndefined(RodMaterial)}",
                $"BeltWidth={BeltWidth}", // Using actual numeric value; change if 0 => "UNDEFINED"
                $"ThumbnailImageFilePath={StrOrUndefined(ThumbnailImageFilePath)}",
                $"QtyRollersAcrossWidth={QtyRollersAcrossWidth}",
                $"FRGCenters={FRGCenters}",
                $"FlightsRollersGrip={StrOrUndefined(FlightsRollersGrip)}",
                $"BeltAccessories={StrOrUndefined(BeltAccessories)}",
                $"FrictionAntiStatic={StrOrUndefined(FrictionAntiStatic)}",
                $"SidePLLaneDV={StrOrUndefined(SidePLLaneDV)}",
                $"UniqueIdentification={StrOrUndefined(UniqueIdentification)}",
                $"IndentCode={StrOrUndefined(IndentCode)}",
                $"SalesOrderNumber={StrOrUndefined(SalesOrderNumber)}",
                $"StartDate={StartDate:yyyy-MM-dd HH:mm:ss}", // Example format; change as needed
                $"AssignedTo={StrOrUndefined(AssignedTo)}",
                $"QueryString={StrOrUndefined(QueryString)}",
                $"PartsList={partsListValue}",
                $"SpecialCaseInfo={specialCaseValue}",
                $"QuoteNumber={StrOrUndefined(QuoteNumber)}",
                $"SugarCRMTaskLink={StrOrUndefined(SugarCRMTaskLink)}",
                $"FinalDrawingFilePath={StrOrUndefined(FinalDrawingFilePath)}",
                $"NumberOfLinks={NumberOfLinks}",
                $"BeltLengthLinearFeet={BeltLengthLinearFeet}",
                $"BeltLengthSquareFeet={BeltLengthSquareFeet}",
                $"StructuredText={StrOrUndefined(StructuredText)}",
                $"CadTemplatePath={StrOrUndefined(CadTemplatePath)}",
                $"ReferenceDrawingPath={StrOrUndefined(ReferenceDrawingPath)}"
            };

            // Combine all lines into a single string with line breaks
            return string.Join(Environment.NewLine, lines);
        }
    }
}