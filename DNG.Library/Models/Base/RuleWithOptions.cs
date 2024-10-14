namespace DrawingNumberGenerator.Library.Models;

public class RuleWithOptions : Rule
{
    public RuleWithOptions(string name,
        string code,
        int maxCharacters)
        : base(name, code, maxCharacters)
    {
    }

    public static RuleWithOptions Create(string name,
        string code,
        int maxCharacters,
        Dictionary<string, string> options)
    {
        return new RuleWithOptions(name,
            code,
            maxCharacters);
    }

    public static string GetNameByCode(string code, Dictionary<string, string> options)
    {
        return options[code];
    }

    public static string GetCodeByName(string name, Dictionary<string, string> options)
    {
        // Use case-insensitive comparison to find the code corresponding to the provided name
        var entry = options.FirstOrDefault(kvp => kvp.Value.Equals(name, StringComparison.OrdinalIgnoreCase));
        return entry.Key;
    }
}