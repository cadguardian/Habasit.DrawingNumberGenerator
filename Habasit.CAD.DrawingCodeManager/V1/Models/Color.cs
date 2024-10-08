namespace Habasit.CAD.DrawingCodeManager.V1.Models;

public class MaterialColor
{
    public string Name { get; set; }
    public string Code { get; set; }

    // Static list of colors and their codes
    public static List<MaterialColor> Colors = new List<MaterialColor>()
    {
        new MaterialColor("Maroon", "01"),
        // Add more based on your data
    };

    public MaterialColor(string name, string code)
    {
        Name = name;
        Code = code;
    }

    // Static method to get color by code
    public static MaterialColor GetColorByCode(string code)
    {
        return Colors.FirstOrDefault(c => c.Code == code);
    }
}
