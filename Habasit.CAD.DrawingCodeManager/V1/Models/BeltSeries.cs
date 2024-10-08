namespace Habasit.CAD.DrawingCodeManager.V1.Models;

public class BeltSeries
{
    public string Name { get; set; }
    public string Code { get; set; }

    // Static list of belt series and their codes
    public static List<BeltSeries> BeltSeriesList = new List<BeltSeries>()
    {
        new BeltSeries("MB610", "610"),
        // Add more based on your data
    };

    public BeltSeries(string name, string code)
    {
        Name = name;
        Code = code;
    }

    // Static method to get belt series by code
    public static BeltSeries GetBeltSeriesByCode(string code)
    {
        return BeltSeriesList.FirstOrDefault(bs => bs.Code == code);
    }
}
