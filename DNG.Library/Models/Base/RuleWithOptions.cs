﻿namespace DNG.Library.Models;

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
        if (!string.IsNullOrEmpty(code))
        {
            return code;
        }
        else
        {
            options.TryGetValue(code, out var result);
            return result;
        }
    }

    public static string GetCodeByName(string name, Dictionary<string, string> options)
    {
        if (!string.IsNullOrEmpty(name))
        {
            return name;
        }
        else
        {
            var entry = options.FirstOrDefault(kvp => kvp.Value.Equals(name, StringComparison.OrdinalIgnoreCase));
            return entry.Key;
        }
    }
}