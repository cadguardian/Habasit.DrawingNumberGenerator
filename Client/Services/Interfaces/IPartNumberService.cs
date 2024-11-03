using DNG.Library.Data;

namespace Client.Services.Interfaces
{
    public interface IPartNumberService
    {
        IEnumerable<KeyValuePair<string, string>> FilterPartNumbers(IDrawingRequest drawingRequest, string universalSearch = "", bool isFreeSearchMode = false);

        Task InitializeAsync(string csvFilePath);
    }
}