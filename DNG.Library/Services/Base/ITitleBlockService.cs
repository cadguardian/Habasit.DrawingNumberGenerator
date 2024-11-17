using DNG.Library.Models;
using DNG.Library.Models.Base;

public interface ITitleBlockService
{
    string GetDrawnDate();

    string GetJobNumber(IDrawingRequest drawingRequest);

    string GetProjectFolderName(string jobNumber, IDrawingNumber drawingNumber);

    string GetTitleLine1(IDrawingRequest drawingRequest, IDrawingNumber drawingNumber);

    string GetTitleLine2(IDrawingRequest drawingRequest);

    string GetTitleLine3(IDrawingRequest drawingRequest);
}