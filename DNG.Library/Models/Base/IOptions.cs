namespace DNG.Library.Models;

public interface IOptions : IRule
{
    public static Dictionary<string, string>? Options { get; }
}