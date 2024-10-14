namespace DrawingNumberGenerator.Library.Models;

public class FrictionAntiStatic : RuleWithOptions, IOptions
{
    public FrictionAntiStatic(string name, string code) : base(
        name,
        code,
        maxCharacters: 1)
    {
    }

    public static Dictionary<string, string> Options => new Dictionary<string, string>()
    {
        ["X"] = "No options",
        ["1"] = "1” Wide Anti Stat Strips",
        ["2"] = "2” Wide Anti Stat Strips",
        ["3"] = "3” Wide Anti Stat Strips",
        ["4"] = "4” Wide Anti Stat Strips",
        ["5"] = "5” Wide Anti Stat Strips",
        ["6"] = "6” Wide Anti Stat Strips",
        ["7"] = "7” Wide Anti Stat Strips",
        ["8"] = "8” Wide Anti Stat Strips",
        ["9"] = "9” Wide Anti Stat Strips",
        ["C"] = "Curved Top High Friction",
        ["F"] = "Flat High Friction",
        ["H"] = "High Friction Flights (M.T.O.)",
        ["T"] = "Rough Top High Friction",
        ["R"] = "Ribbed High Friction"
    };
}