namespace DrawingNumberGenerator.Library.Models;

public class Rule : IRule
{
    public string Name { get; }
    public string Code { get; }
    public int MaxCharacters { get; }

    public Rule(string name, string code, int maxCharacters)
    {
        Name = name;
        Code = code;
        MaxCharacters = maxCharacters;
    }
}