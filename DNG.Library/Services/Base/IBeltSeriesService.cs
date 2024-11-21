namespace DNG.Library.Services.Base;

public interface IBeltSeriesService
{
    Task<List<string>> LoadImageFilesAsync(HttpClient httpClient);

    string? GetImageFileNameForBeltSeries(string beltSeriesCode, List<string> imageFileNames);

    string GetBeltSeriesType(string beltSeriesCode);
}