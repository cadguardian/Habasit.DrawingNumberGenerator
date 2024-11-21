using DNG.Library.Models.Base;

namespace DNG.Library.Services.Base;

public interface IDrawingRequestProcessor
{
    IStateContainer? State { get; set; }
    IPartNumberService? PartNumberService { get; }

    void GenerateDrawingNumber();

    string SerializeDrawingData();
}