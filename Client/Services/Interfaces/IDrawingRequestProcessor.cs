using DNG.Library.Data;
using DNG.Library.Models;

namespace Client.Services.Interfaces
{
    public interface IDrawingRequestProcessor
    {
        IDrawingNumber DrawingNumber { get; set; }
        IDrawingRequest DrawingRequest { get; set; }

        void GenerateDrawingNumber();

        string SerializeDrawingData();
    }
}