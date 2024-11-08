namespace DNG.Library.Models.BeltSpecs;

public class RodMaterial : RuleWithOptions, IOptions
{
    public RodMaterial(string name, string code) : base(
        name,
        code,
        maxCharacters: 1)
    {
    }

    public static Dictionary<string, string> Options => new Dictionary<string, string>
    {
        ["A"] = "POM (Acetal) Rods",
        ["B"] = "Blue PA Rods",
        ["C"] = "Chlorine Res. Rods",
        ["D"] = "PA Black",
        ["G"] = "Reinforced PA Rods",
        ["K"] = "White PBT Rods",
        ["L"] = "Blue POM Rods",
        ["N"] = "PA (Nylon) Rods",
        ["P"] = "PP Rods",
        ["Q"] = "Metal Detectable PA",
        ["R"] = "PE Rods",
        ["S"] = "Stainless Steel",
        ["T"] = "ST Rods",
        ["U"] = "UHMW",
        ["Y"] = "Natural PA Rods",
        ["Z"] = "Black PBT Rods"
    };
}