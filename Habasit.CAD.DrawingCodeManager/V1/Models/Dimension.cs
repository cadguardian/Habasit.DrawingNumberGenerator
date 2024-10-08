namespace Habasit.CAD.DrawingCodeManager.V1.Models;

public class Dimension
{
    public string WidthCode { get; set; }
    public string HeightCode { get; set; }

    // Static list for dimensions mapping codes to actual sizes (in inches)
    public static Dictionary<string, double> Widths = new Dictionary<string, double>()
    {
        { "B", 0.5 }, { "C", 0.75 }, { "D", 1.0 }, { "E", 1.25 },
        { "F", 1.5 }, { "G", 1.75 }, { "L", 2.0 }, { "M", 2.25 },
        // Add more based on your data
    };

    public static Dictionary<string, double> Heights = new Dictionary<string, double>()
    {
        { "B", 0.5 }, { "C", 0.75 }, { "D", 1.0 }, { "E", 1.25 },
        { "F", 1.5 }, { "G", 1.75 }, { "L", 2.0 }, { "M", 2.25 },
        // Add more based on your data
    };

    public Dimension(string widthCode, string heightCode)
    {
        WidthCode = widthCode;
        HeightCode = heightCode;
    }

    // Get code representation
    public string GetCode()
    {
        return $"{WidthCode}{HeightCode}";
    }

    // Helper to get the dimension in inches
    public double GetWidthInInches() => Widths.ContainsKey(WidthCode) ? Widths[WidthCode] : 0;
    public double GetHeightInInches() => Heights.ContainsKey(HeightCode) ? Heights[HeightCode] : 0;
}
