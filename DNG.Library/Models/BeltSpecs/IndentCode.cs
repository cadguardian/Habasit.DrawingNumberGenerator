namespace DNG.Library.Models.BeltSpecs;

public class IndentCode : RuleWithOptions, IOptions
{
    public static Dictionary<string, string> Options => new Dictionary<string, string>
    {
        ["a"] = "0.5",
        ["b"] = "0.75",
        ["c"] = "1.0",
        ["d"] = "1.25",
        ["e"] = "1.5",
        ["f"] = "1.75",
        ["g"] = "2.0",
        ["h"] = "2.25",
        ["i"] = "2.5",
        ["j"] = "2.75",
        ["k"] = "3.0",
        ["l"] = "3.25",
        ["m"] = "3.5",
        ["n"] = "3.75",
        ["o"] = "4.0",
        ["p"] = "4.25",
        ["q"] = "4.5",
        ["r"] = "4.75",
        ["s"] = "5.0",
        ["t"] = "5.25",
        ["u"] = "5.5",
        ["v"] = "5.75",
        ["w"] = "6.0",
        ["x"] = "6.25",
        ["y"] = "6.5",
        ["z"] = "6.75",
        ["aa"] = "7.0",
        ["ab"] = "7.25",
        ["ac"] = "7.5",
        ["ad"] = "7.75",
        ["ae"] = "8.0",
        ["af"] = "8.25",
        ["ag"] = "8.5",
        ["ah"] = "8.75",
        ["ai"] = "9.0",
        ["aj"] = "9.25",
        ["ak"] = "9.5",
        ["al"] = "9.75",
        ["am"] = "10.0",
        ["an"] = "10.25",
        ["ao"] = "10.5",
        ["ap"] = "10.75",
        ["aq"] = "11.0",
        ["ar"] = "11.25",
        ["as"] = "11.5",
        ["at"] = "11.75",
        ["au"] = "12.0"
    };

    public IndentCode(string name, string code) : base(
        name,
        code,
        maxCharacters: 2)
    { }

    public decimal GetDimension(string code)
    {
        string codeLowerCase = code.ToLower();
        return Options.ContainsKey(codeLowerCase) ?
            decimal.Parse(Options[codeLowerCase]) :
            throw new ArgumentException("Invalid code");
    }

    public string GetCodeByIndentDimension(decimal indentDimension)
    {
        string formattedDimension = FormatDecimal(indentDimension);
        return Options.FirstOrDefault(
            kvp => kvp.Value == formattedDimension).Key
               ?? throw new ArgumentException("Dimension not found");
    }

    private static string FormatDecimal(decimal dimension)
    {
        // Format the decimal to the string representation used in the dictionary
        return dimension % 1 == 0
            ? $"{dimension:0.#}"  // Format as 0.# if no decimal places (e.g., 1.0)
            : $"{dimension:0.##}"; // Format as 0.## if there are decimal places (e.g., 1.25)
    }
}