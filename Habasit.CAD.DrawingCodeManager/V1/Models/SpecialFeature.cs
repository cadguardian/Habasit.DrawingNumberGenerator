namespace Habasit.CAD.DrawingCodeManager.V1.Models;
public class SpecialFeature
{
    public string Name { get; set; }
    public string Code { get; set; }

    // Static list of special features and their codes
    public static List<SpecialFeature> SpecialFeatures = new List<SpecialFeature>()
    {
        new SpecialFeature("UV Resistant", "UV"),
        // Add more based on your data
    };

    public SpecialFeature(string name, string code)
    {
        Name = name;
        Code = code;
    }

    // Static method to get special feature by code
    public static SpecialFeature GetFeatureByCode(string code)
    {
        return SpecialFeatures.FirstOrDefault(sf => sf.Code == code);
    }
}
