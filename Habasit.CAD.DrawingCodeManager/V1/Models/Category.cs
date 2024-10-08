namespace Habasit.CAD.DrawingCodeManager.V1.Models;
public class Category
{

    public static List<Category> Options = new List<Category>();
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public static Category GetByCode(string code)
    {
        return Options.FirstOrDefault(bs => bs.Code == code)!;
    }
}
