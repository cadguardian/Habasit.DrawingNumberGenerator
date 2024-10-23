namespace DNG.Library.Models;

public class BeltMaterial : RuleWithOptions, IOptions
{
    public BeltMaterial(string name, string code)
        : base(name, code, maxCharacters: 1)
    {
    }

    public static Dictionary<string, string> Options => new Dictionary<string, string>()
    {
        ["A"] = "POM (Acetal)",
        ["B"] = "PBT",
        ["E"] = "Polyethylene",
        ["N"] = "PA (Nylon)",
        ["P"] = "Polypropylene",
        ["Q"] = "Polycarbonate",
        ["U"] = "Urethane",
        ["ST"] = "ST Material"
    };
}