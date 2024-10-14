namespace DrawingNumberGenerator.Library.Models;

public interface IRule
{
    string Code { get; }
    int MaxCharacters { get; }
    string Name { get; }
}