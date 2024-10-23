using DNG.Library.Data;
using DNG.Library.Models;

namespace Client.Services
{
    public interface IDrawingRequestProcessor
    {
        IDrawingNumber DrawingNumber { get; }
        IDrawingRequest DrawingRequest { get; }

        void GenerateDrawingNumber();
    }
}