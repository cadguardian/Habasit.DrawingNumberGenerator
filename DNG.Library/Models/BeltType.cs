namespace DrawingNumberGenerator.Library.Models;

public class BeltType : RuleWithOptions, IOptions
{
    public static Dictionary<string, string> Options => new Dictionary<string, string>()
    {
        ["M"] = "Standard Belt",
        ["S"] = "Spiral Belt",
        ["A"] = "ActivXchange",
        ["G"] = "Grip Top Belt",
        ["T"] = "Hold Down Tabs",
        ["L"] = "Long Hold Down"
    };

    public BeltType(string name, string code) : base(
        name,
        code,
        maxCharacters: 1)
    { }

    public static BeltType Test()
    {
        var name = "Standard Belt";
        var code = "S";
        var maxCharacters = code.Length;

        return BeltType.Create(name, code);
    }

    public static BeltType Create(string name, string code)
    {
        return new BeltType(name, code);
    }
}