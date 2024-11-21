using DNG.Library.Models;
using DNG.Library.Models.Base;
using DNG.Library.Services.Base;

namespace DNG.Library.Services;

public class TitleBlockService : ITitleBlockService
{
    public string GetDrawnDate()
    {
        return DateTime.Now.ToString("MM/dd/yyyy");
    }

    public string GetJobNumber(IDrawingRequest drawingRequest)
    {
        if (!string.IsNullOrWhiteSpace(drawingRequest.SalesOrderNumber))
        {
            return $"SO{drawingRequest.SalesOrderNumber}";
        }
        else if (!string.IsNullOrWhiteSpace(drawingRequest.PurchaseOrderNumber))
        {
            return $"PO{drawingRequest.PurchaseOrderNumber}";
        }
        else if (!string.IsNullOrWhiteSpace(drawingRequest.QuoteNumber))
        {
            return $"Q{drawingRequest.QuoteNumber}";
        }
        return "";
    }

    public string GetProjectFolderName(string jobNumber, IDrawingNumber drawingNumber)
    {
        return $"{jobNumber} - {drawingNumber.BeltTypeCode}{drawingNumber.BeltSeriesCode}";
    }

    public string GetTitleLine1(IDrawingRequest drawingRequest, IDrawingNumber drawingNumber)
    {
        return string.Join(" ",
            drawingNumber.BeltTypeCode + drawingRequest.BeltSeries,
            drawingRequest.Color,
            drawingRequest.Material,
            drawingRequest.BeltWidth > 0 ? $"{drawingRequest.BeltWidth}\" WIDE" : "",
            drawingRequest.NumberOfLinks > 0 ? $"({drawingRequest.NumberOfLinks}L)" : "").ToUpper();
    }

    public string GetTitleLine2(IDrawingRequest drawingRequest)
    {
        return string.Join(" ",
            drawingRequest.FlightsRollersGrip == "No Flights" ? "" : drawingRequest.FlightsRollersGrip,
            drawingRequest.FRGCenters > 0 ? $"ON {drawingRequest.FRGCenters}\" CENTERS" : "").ToUpper();
    }

    public string GetTitleLine3(IDrawingRequest drawingRequest)
    {
        var indent = !string.IsNullOrEmpty(drawingRequest.IndentCode) ?
            $"{drawingRequest.IndentCode}\" INDENT" : "";

        var result = string.Join(" ", indent, drawingRequest.RodMaterial).ToUpper();

        return result;
    }
}