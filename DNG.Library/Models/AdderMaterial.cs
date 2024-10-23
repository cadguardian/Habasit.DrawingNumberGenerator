using DNG.Library.Models;

namespace DNG.Library.Models;

public class AdderMaterial : RuleWithOptions, IOptions
{
    public AdderMaterial(string name, string code) : base(
        name,
        code,
        maxCharacters: 1)
    { }

    public static Dictionary<string, string> Options => new()
    {
        ["-"] = "No Adder",
        ["A"] = "Acid Resistant",
        //  ["B"] = "", // Placeholder for an unspecified option
        ["C"] = "Chlorine Res.",
        // ["D"] = "", // Placeholder for an unspecified option
        ["E"] = "Elec. Cond.",
        ["F"] = "Talc Filled",
        ["G"] = "Reinforced",
        ["H"] = "Habaguard",
        ["I"] = "Impact/Cut Res.",
        ["J"] = "Fatigue Res.",
        ["K"] = "Wear Resistant",
        ["L"] = "RE Non-Stick",
        ["M"] = "Metal Det.",
        // ["N"] = "", // Placeholder for an unspecified option
        // ["O"] = "", // Placeholder for an unspecified option
        ["P"] = "Low Friction",
        ["Q"] = "Special Mat'l",
        ["R"] = "Flame Ret.",
        ["S"] = "Submersible",
        ["T"] = "High Temp.",
        ["U"] = "ULow Friction",
        ["V"] = "UV Resistant",
        ["W"] = "H-Water Res.",
        ["X"] = "X-Ray Det.",
        ["Y"] = "Anti-Static",
        ["Z"] = "EC Flame Ret."
    };
}