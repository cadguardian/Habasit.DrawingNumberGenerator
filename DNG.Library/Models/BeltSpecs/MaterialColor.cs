namespace DNG.Library.Models.BeltSpecs;

public class MaterialColor : RuleWithOptions, IOptions
{
    public MaterialColor(string name, string code) : base(
        name,
        code,
        maxCharacters: 1)
    {
    }

    public static Dictionary<string, string> Options => new Dictionary<string, string>
    {
        ["B"] = "Blue",
        ["C"] = "Natural",
        ["D"] = "Black",
        ["E"] = "Beige",
        ["F"] = "Green",
        ["G"] = "Gray",
        ["L"] = "LT Blue",
        ["M"] = "Maroon",
        ["N"] = "Brown",
        ["R"] = "Red",
        ["T"] = "Tan",
        ["W"] = "White",
        ["Y"] = "Yellow",
        ["X"] = "WHI"
    };
}