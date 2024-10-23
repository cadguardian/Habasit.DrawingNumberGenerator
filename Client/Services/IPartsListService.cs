
public interface IPartsListService
{
    IEnumerable<KeyValuePair<string, string>> FilterParts(string beltType, string beltSeries, string color, string material);
}