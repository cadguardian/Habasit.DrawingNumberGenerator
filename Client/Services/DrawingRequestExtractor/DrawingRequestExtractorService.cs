using DNG.Library.Models;
using System.Text.RegularExpressions;

namespace DNG.Library.Models
{
    public class DrawingRequestExtractorService
    {
        public DrawingRequest ExtractDrawingRequestFromEmail(string emailContent)
        {
            var drawingRequest = new DrawingRequest();

            // Extracting basic information using regex patterns
            drawingRequest.BeltType = ExtractBeltType(emailContent);
            drawingRequest.BeltSeries = ExtractBeltSeries(emailContent);
            drawingRequest.Color = ExtractColor(emailContent);
            drawingRequest.Material = ExtractMaterial(emailContent);
            drawingRequest.RodMaterial = ExtractRodMaterial(emailContent);
            drawingRequest.BeltWidth = ExtractBeltWidth(emailContent);
            drawingRequest.FRGCenters = ExtractFRGCenters(emailContent);
            drawingRequest.FlightsRollersGrip = ExtractFlightsRollersGrip(emailContent);
            drawingRequest.IndentCode = ExtractIndentCode(emailContent);

            // Special Case Info
            drawingRequest.SpecialCaseInfo = ExtractSpecialCaseInfo(emailContent);

            // Extract CRM Data
            drawingRequest.StartDate = ExtractStartDate(emailContent);
            drawingRequest.AssignedTo = ExtractAssignedTo(emailContent);

            return drawingRequest;
        }

        private string ExtractBeltType(string emailContent) =>
            ExtractPattern(emailContent, @"BELT:\s*([^\n]+)");

        private string ExtractBeltSeries(string emailContent) =>
            ExtractPattern(emailContent, @"BELT:\s*([^\s]+)");

        private string ExtractColor(string emailContent) =>
            ExtractPattern(emailContent, @"COLOR:\s*([^\n]+)");

        private string ExtractMaterial(string emailContent) =>
            ExtractPattern(emailContent, @"MATERIAL:\s*([^\n]+)");

        private string ExtractRodMaterial(string emailContent) =>
            ExtractPattern(emailContent, @"RODS:\s*([^\n]+)");

        private decimal ExtractBeltWidth(string emailContent)
        {
            var widthText = ExtractPattern(emailContent, @"BELT WIDTH:\s*([\d\.]+)");
            return decimal.TryParse(widthText, out var width) ? width : 0;
        }

        private decimal ExtractFRGCenters(string emailContent)
        {
            var spacingText = ExtractPattern(emailContent, @"SPACING:\s*([\d\.]+)\s*IN");
            return decimal.TryParse(spacingText, out var spacing) ? spacing : 0;
        }

        private string ExtractFlightsRollersGrip(string emailContent) =>
            ExtractPattern(emailContent, @"FLIGHT:\s*([^\n]+)");

        private string ExtractIndentCode(string emailContent) =>
            ExtractPattern(emailContent, @"INDENT:\s*([^\n]+)");

        private Dictionary<string, string> ExtractSpecialCaseInfo(string emailContent)
        {
            var specialInfo = new Dictionary<string, string>();

            // Example: Extract 'SANICLIP: Qty: 1 SPACING: 0'
            var pattern = @"(\w+):\s*([^:]+)";
            var matches = Regex.Matches(emailContent, pattern);

            foreach (Match match in matches)
            {
                var key = match.Groups[1].Value;
                var value = match.Groups[2].Value;
                specialInfo[key] = value;
            }

            return specialInfo;
        }

        private string ExtractSugarCRMSubjectLine(string emailContent) =>
            ExtractPattern(emailContent, @"Subject:\s*([^\n]+)");

        private DateTime ExtractStartDate(string emailContent)
        {
            var dateText = ExtractPattern(emailContent, @"Start Date\s*([\d\-:]+)");
            return DateTime.TryParse(dateText, out var date) ? date : DateTime.MinValue;
        }

        private string ExtractAssignedTo(string emailContent) =>
            ExtractPattern(emailContent, @"Assigned to\s*([^\n]+)");

        private string ExtractPattern(string input, string pattern)
        {
            var match = Regex.Match(input, pattern);
            return match.Success ? match.Groups[1].Value.Trim() : string.Empty;
        }
    }
}