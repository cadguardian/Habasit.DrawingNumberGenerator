using DNG.Library.Data;

namespace Client.Services
{
    public interface IPartNumberService
    {
        IEnumerable<KeyValuePair<string, string>> FilterPartNumbers(IDrawingRequest drawingRequest, string universalSearch = "");
        Task InitializeAsync(string url);
    }
}