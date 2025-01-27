namespace DNG.Library.Models.BeltSpecs;

public class BeltAccessories : RuleWithOptions, IOptions
{
    public BeltAccessories(string name, string code) : base(
              name,
              code,
              maxCharacters: 2)
    {
    }

    public static Dictionary<string, string> Options => new Dictionary<string, string>()
    {
        ["XX"] = "No options",
        ["LD"] = "Lane Dividers",
        ["HD"] = "Hold Down Device",
        ["FR"] = "Flight Retractable",
        ["FC"] = "Flights Cam",
        ["FP"] = "Flights Pop-up",
        ["PG"] = "1.8 Rad. Exp. Plug",
        ["PW"] = "2.0 Rad. Exp. Plug",
        ["PB"] = "2.2 Rad. Exp. Plug",
        ["PF"] = "2.5 Rad. Exp. Plug",
        ["PD"] = "3.0 Rad. Exp. Plug",
        ["SC"] = "SaniClip",
        ["PC"] = "Pacer Strip"
    };
}