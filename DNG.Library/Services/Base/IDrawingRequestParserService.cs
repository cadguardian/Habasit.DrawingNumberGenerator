using DNG.Library.Models.Base;

namespace DNG.Library.Services.Base;

public interface IDrawingRequestParserService
{
    Task<IDrawingRequest> ParseDrawingRequestAsync(string input);
}