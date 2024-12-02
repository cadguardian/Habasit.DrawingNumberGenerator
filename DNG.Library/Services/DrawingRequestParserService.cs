// Services/DrawingRequestParserService.cs
using DNG.Library.Models;
using DNG.Library.Models.Base;
using DNG.Library.Services.Base;
using System.Text.RegularExpressions;

namespace DNG.Library.Services;

public class DrawingRequestParserService : IDrawingRequestParserService
{
    public async Task<IDrawingRequest> ParseDrawingRequestAsync(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input cannot be null or empty.", nameof(input));

        var drawingRequest = new DrawingRequest(); // Assuming you have a concrete implementation

        var lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            await ParseLineAsync(line.Trim(), drawingRequest);
        }

        return drawingRequest;
    }

    private Task ParseLineAsync(string line, IDrawingRequest drawingRequest)
    {
        // Determine which parameter the line corresponds to
        if (line.StartsWith("BELT:", StringComparison.OrdinalIgnoreCase))
            ParseBeltLine(line, drawingRequest);
        else if (line.StartsWith("HOLDDOWN:", StringComparison.OrdinalIgnoreCase))
            ParseHoldDownLine(line, drawingRequest);
        else if (line.StartsWith("RODS:", StringComparison.OrdinalIgnoreCase) || line.StartsWith("NonStd RODS:", StringComparison.OrdinalIgnoreCase))
            ParseRodsLine(line, drawingRequest);
        else if (line.StartsWith("BELT WIDTH:", StringComparison.OrdinalIgnoreCase))
            ParseBeltWidthLine(line, drawingRequest);
        else if (line.StartsWith("FLIGHT:", StringComparison.OrdinalIgnoreCase))
            ParseFlightLine(line, drawingRequest);
        else if (line.StartsWith("SPACING:", StringComparison.OrdinalIgnoreCase) || Regex.IsMatch(line, @"\bspacing\b", RegexOptions.IgnoreCase))
            ParseSpacingLine(line, drawingRequest);
        else if (line.StartsWith("INDENT:", StringComparison.OrdinalIgnoreCase))
            ParseIndentLine(line, drawingRequest);
        else if (line.StartsWith("CENTER NOTCH:", StringComparison.OrdinalIgnoreCase))
            ParseCenterNotchLine(line, drawingRequest);
        else if (line.StartsWith("SANICLIP:", StringComparison.OrdinalIgnoreCase))
            ParseSaniClipLine(line, drawingRequest);
        // Add additional parsers as needed
        else
            HandleUnmatchedLine(line, drawingRequest);

        return Task.CompletedTask;
    }

    private void ParseBeltLine(string line, IDrawingRequest drawingRequest)
    {
        // Example: BELT: SM605 Smooth Mesh Acetal White
        var pattern = @"BELT:\s*(\S+)\s+([A-Za-z\s]+)\s+([A-Za-z]+)\s+([A-Za-z]+)";
        var match = Regex.Match(line, pattern, RegexOptions.IgnoreCase);
        if (match.Success)
        {
            drawingRequest.BeltSeries = match.Groups[1].Value.Trim();
            drawingRequest.BeltType = match.Groups[2].Value.Trim();
            drawingRequest.Material = match.Groups[3].Value.Trim();
            drawingRequest.Color = match.Groups[4].Value.Trim();
        }
        else
        {
            // Attempt flexible parsing
            var parts = line.Substring(5).Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 4)
            {
                drawingRequest.BeltSeries = parts[0];
                drawingRequest.BeltType = parts[1] + " " + parts[2];
                drawingRequest.Material = parts[3];
                if (parts.Length >= 5)
                    drawingRequest.Color = string.Join(" ", parts.Skip(4));
            }
        }
    }

    private void ParseHoldDownLine(string line, IDrawingRequest drawingRequest)
    {
        // Example: HOLDDOWN: Both Edges Every Row
        // Currently, HOLDDOWN defines Belt Type and Accessories, but specifics are not provided
        // Implement as needed
        drawingRequest.BeltAccessories = line.Substring("HOLDDOWN:".Length).Trim();
    }

    private void ParseRodsLine(string line, IDrawingRequest drawingRequest)
    {
        // Examples:
        // RODS: Nylon
        // NonStd RODS: SS & Acetal
        var rodsPart = line.Contains("NonStd", StringComparison.OrdinalIgnoreCase) ?
                       line.Substring(line.IndexOf("RODS:", StringComparison.OrdinalIgnoreCase) + 5).Trim() :
                       line.Substring(line.IndexOf("RODS:") + 5).Trim();

        // Split rods by common delimiters
        var rods = rodsPart.Split(new[] { ',', '&', '/', ';' }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(r => r.Trim()).ToList();

        drawingRequest.RodMaterial = string.Join(", ", rods);

        // Handle unusual cases (e.g., multiple rod materials)
        if (rods.Count > 1)
        {
            // Implement alert or warning logic here, possibly via a property
            drawingRequest.SpecialCaseInfo["MultipleRods"] = "Multiple rod materials detected: " + drawingRequest.RodMaterial;
        }
    }

    private void ParseBeltWidthLine(string line, IDrawingRequest drawingRequest)
    {
        // Example: BELT WIDTH: 16.0 IN +/- 0.10 (32) Links
        var pattern = @"BELT WIDTH:\s*(\d+(\.\d+)?)\s*IN.*\((\d+)\)\s*Links";
        var match = Regex.Match(line, pattern, RegexOptions.IgnoreCase);
        if (match.Success)
        {
            if (decimal.TryParse(match.Groups[1].Value, out var width))
                drawingRequest.BeltWidth = width;

            if (int.TryParse(match.Groups[3].Value, out var links))
                drawingRequest.NumberOfLinks = links;
        }
        else
        {
            // Flexible parsing
            var inMatch = Regex.Match(line, @"(\d+(\.\d+)?)\s*IN", RegexOptions.IgnoreCase);
            if (inMatch.Success && decimal.TryParse(inMatch.Groups[1].Value, out var width))
                drawingRequest.BeltWidth = width;

            var linksMatch = Regex.Match(line, @"\((\d+)\)\s*Links", RegexOptions.IgnoreCase);
            if (linksMatch.Success && int.TryParse(linksMatch.Groups[1].Value, out var links))
                drawingRequest.NumberOfLinks = links;
        }
    }

    private void ParseFlightLine(string line, IDrawingRequest drawingRequest)
    {
        // Example: FLIGHT: 3.00 IN. High CUT Streamline Polypropylene Gray
        var pattern = @"FLIGHT:\s*(\d+(\.\d+)?)\s*IN\.*\s*(.*)";
        var match = Regex.Match(line, pattern, RegexOptions.IgnoreCase);
        if (match.Success)
        {
            var flightSize = match.Groups[1].Value.Trim();
            var flightDetails = match.Groups[3].Value.Trim();

            drawingRequest.FlightsRollersGrip = $"{flightSize} IN {flightDetails}";

            // Additional logic: if "CUT" is present
            if (flightDetails.Contains("CUT", StringComparison.OrdinalIgnoreCase))
            {
                // Implement logic to handle CUT flights
                drawingRequest.SpecialCaseInfo["FlightCut"] = $"Flight will be cut from {flightSize} IN.";
            }
        }
        else
        {
            // Flexible parsing
            var inMatch = Regex.Match(line, @"(\d+(\.\d+)?)\s*IN\.*\s*(.*)", RegexOptions.IgnoreCase);
            if (inMatch.Success)
            {
                var flightSize = inMatch.Groups[1].Value.Trim();
                var flightDetails = inMatch.Groups[3].Value.Trim();

                drawingRequest.FlightsRollersGrip = $"{flightSize} IN {flightDetails}";

                if (flightDetails.Contains("CUT", StringComparison.OrdinalIgnoreCase))
                {
                    drawingRequest.SpecialCaseInfo["FlightCut"] = $"Flight will be cut from {flightSize} IN.";
                }
            }
        }
    }

    private void ParseSpacingLine(string line, IDrawingRequest drawingRequest)
    {
        // Examples:
        // SPACING: 2.00 IN
        // 4.00 IN Spacing

        var pattern = @"SPACING:\s*(\d+(\.\d+)?)\s*IN";
        var match = Regex.Match(line, pattern, RegexOptions.IgnoreCase);
        if (match.Success)
        {
            if (decimal.TryParse(match.Groups[1].Value, out var spacing))
                drawingRequest.FRGCenters = spacing;
        }
        else
        {
            // Flexible parsing
            var inMatch = Regex.Match(line, @"(\d+(\.\d+)?)\s*IN\s*spacing", RegexOptions.IgnoreCase);
            if (inMatch.Success && decimal.TryParse(inMatch.Groups[1].Value, out var spacing))
            {
                drawingRequest.FRGCenters = spacing;
            }
        }
    }

    private void ParseIndentLine(string line, IDrawingRequest drawingRequest)
    {
        // Example: INDENT: 1.30 IN
        var pattern = @"INDENT:\s*(\d+(\.\d+)?)\s*IN";
        var match = Regex.Match(line, pattern, RegexOptions.IgnoreCase);
        if (match.Success)
        {
            if (decimal.TryParse(match.Groups[1].Value, out var indent))
                drawingRequest.IndentCode = indent.ToString();
        }
        else
        {
            // Flexible parsing
            var inMatch = Regex.Match(line, @"(\d+(\.\d+)?)\s*IN", RegexOptions.IgnoreCase);
            if (inMatch.Success && decimal.TryParse(inMatch.Groups[1].Value, out var indent))
            {
                drawingRequest.IndentCode = indent.ToString();
            }
        }
    }

    // todo: capture in drawing request data model
    private void ParseCenterNotchLine(string line, IDrawingRequest drawingRequest)
    {
        // Ensure the line starts with "CENTER NOTCH:"
        if (!line.StartsWith("CENTER NOTCH:", StringComparison.OrdinalIgnoreCase))
            return;

        // Example: CENTER NOTCH: 2.00 IN
        var pattern = @"CENTER NOTCH:\s*(\d+(\.\d+)?)\s*IN";
        var match = Regex.Match(line, pattern, RegexOptions.IgnoreCase);
        if (match.Success)
        {
            //if (decimal.TryParse(match.Groups[1].Value, out var centerNotch))
            //    drawingRequest.FRGCenters = centerNotch;
        }
        // No else block needed since we only parse explicit lines
    }

    // todo: capture in drawing request data model
    private void ParseSaniClipLine(string line, IDrawingRequest drawingRequest)
    {
        // Ensure the line starts with "SANICLIP:"
        if (!line.StartsWith("SANICLIP:", StringComparison.OrdinalIgnoreCase))
            return;

        // Example: SANICLIP: Qty: 5 SPACING: 120.00 (Inches)
        var qtyPattern = @"Qty:\s*(\d+)";
        var spacingPattern = @"SPACING:\s*(\d+(\.\d+)?)";

        var qtyMatch = Regex.Match(line, qtyPattern, RegexOptions.IgnoreCase);
        var spacingMatch = Regex.Match(line, spacingPattern, RegexOptions.IgnoreCase);

        //if (qtyMatch.Success && int.TryParse(qtyMatch.Groups[1].Value, out var qty))
        //    drawingRequest.QtyRollersAcrossWidth = qty;

        //if (spacingMatch.Success && decimal.TryParse(spacingMatch.Groups[1].Value, out var spacing))
        //    drawingRequest.FRGCenters = spacing;

        // Optionally, handle cases where SANICLIP information might be incomplete
    }

    // todo: improve this
    private void HandleUnmatchedLine(string line, IDrawingRequest drawingRequest)
    {
        // Attempt to parse parameters without the "PARAM:" prefix
        // Example: 5.00 IN spacing
        var spacingMatch = Regex.Match(line, @"(\d+(\.\d+)?)\s*IN\s*spacing", RegexOptions.IgnoreCase);
        if (spacingMatch.Success && decimal.TryParse(spacingMatch.Groups[1].Value, out var spacing))
        {
            //drawingRequest.FRGCenters = spacing;
            return;
        }

        // Add more flexible parsing as needed
        // You can also log or store unmatched lines for further analysis
        drawingRequest.SpecialCaseInfo["UnmatchedLine"] = line;
    }
}