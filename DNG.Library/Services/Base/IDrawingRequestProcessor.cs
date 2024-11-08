using DNG.Library.Data;
using DNG.Library.Models;

public interface IDrawingRequestProcessor
{
    IDrawingNumber DrawingNumber { get; set; }
    IDrawingRequest DrawingRequest { get; set; }

    void GenerateDrawingNumber();

    string SerializeDrawingData();
}