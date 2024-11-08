using DNG.Library.Data;
using DNG.Library.Models;

public interface ITitleBlockService
{
    string GetDrawnDate();

    string GetJobNumber(IDrawingRequest drawingRequest);

    string GetProjectFolderName(string jobNumber, IDrawingNumber drawingNumber);

    string GetTitleLine1(IDrawingRequest drawingRequest, IDrawingNumber drawingNumber);

    string GetTitleLine2(IDrawingRequest drawingRequest);

    string GetTitleLine3(IDrawingRequest drawingRequest);
}