namespace Habasit.CAD.DrawingCodeManager.V1.Models;
public class RodMaterial
{
    public string Name { get; set; }
    public string Code { get; set; }

    // Static list of rod materials and their codes
    public static List<RodMaterial> RodMaterials = new List<RodMaterial>()
    {
        new RodMaterial("Stainless Steel", "SS"),
        // Add more based on your data
    };

    public RodMaterial(string name, string code)
    {
        Name = name;
        Code = code;
    }

    // Static method to get rod material by code
    public static RodMaterial GetRodMaterialByCode(string code)
    {
        return RodMaterials.FirstOrDefault(rm => rm.Code == code);
    }
}
