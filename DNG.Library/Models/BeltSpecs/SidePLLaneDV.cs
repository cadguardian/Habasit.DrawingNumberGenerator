namespace DNG.Library.Models.BeltSpecs;

public class SidePLLaneDV : RuleWithOptions, IOptions
{
    public SidePLLaneDV(string name, string code) : base(
        name,
        code,
        maxCharacters: 1)
    {
    }

    public static Dictionary<string, string> Options => new Dictionary<string, string>()
    {
        ["X"] = "No options",
        ["0"] = "Belt High SP",
        ["1"] = "1” High SP",
        ["2"] = "2” High SP",
        ["3"] = "3” High SP",
        ["4"] = "4” High SP",
        ["A"] = "½” Lane Dividers",
        ["D"] = "1” Lane Dividers",
        ["H"] = "½” High SP",
        ["J"] = "1-½” High SP",
        ["L"] = "2” Lane Dividers",
        ["P"] = "4” Lane Dividers",
        ["R"] = "w/ Ribbed Side Plates",
        ["S"] = "Special Cut Side Plates",
        ["T"] = "Tabs – See dwg for placement",
        ["U"] = "Special Cut SP/LD"
    };
}