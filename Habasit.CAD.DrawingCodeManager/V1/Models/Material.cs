namespace Habasit.CAD.DrawingCodeManager.V1.Models;
public class Material
{
    public string Name { get; set; }
    public string Code { get; set; }

    // Static list of materials and their codes
    public static List<Material> Materials = new List<Material>()
    {
        new Material("POM (Acetal)", "01"),
        // Add more based on your data
    };

    public Material(string name, string code)
    {
        Name = name;
        Code = code;
    }

    // Static method to get material by code
    public static Material GetMaterialByCode(string code)
    {
        return Materials.FirstOrDefault(m => m.Code == code);
    }
}
