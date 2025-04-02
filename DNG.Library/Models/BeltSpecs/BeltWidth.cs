using DNG.Library.Models;

public class BeltWidth : IRule, INumberRule
{
    public static BeltWidth Create(decimal width)
    {
        return new BeltWidth(width);
    }

    private int HundredsPlace { get; }
    public int TensPlace { get; }
    public int OnesPlace { get; }
    public int DecimalPlace { get; }

    public string Code { get; }

    public int MaxCharacters { get; }

    public string Name => string.Empty;

    private const decimal MaxWidth = 999.9m; // Maximum allowed width

    public BeltWidth(decimal width)
    {
        if (width > MaxWidth)
        {
            throw new ArgumentOutOfRangeException(nameof(width), $"Width cannot exceed {MaxWidth}");
        }

        HundredsPlace = CalculateHundredsPlace(width);
        TensPlace = CalculateTensPlace(width);
        OnesPlace = CalculateOnesPlace(width);
        DecimalPlace = CalculateDecimalPlace(width);
        Code = GetBeltWidth();
        MaxCharacters = 4;
    }

    private int CalculateHundredsPlace(decimal width)
    {
        return (int)(width / 100);
    }

    public int CalculateTensPlace(decimal width)
    {
        int remainingWidth = (int)width % 100;
        return remainingWidth / 10;
    }

    public int CalculateOnesPlace(decimal width)
    {
        int remainingWidth = (int)width % 100;
        return remainingWidth % 10;
    }

    public int CalculateDecimalPlace(decimal width)
    {
        return (int)((width - Math.Floor(width)) * 10);
    }

    public string GetBeltWidth()
    {
        return $"{HundredsPlace}{TensPlace}{OnesPlace}{DecimalPlace}";
    }

    public static string ConvertToInches(string beltWidthCode)
    {
        if (string.IsNullOrWhiteSpace(beltWidthCode) || beltWidthCode.Length != 4 || !int.TryParse(beltWidthCode, out int numericValue))
        {
            throw new ArgumentException("Invalid belt width code. It must be a 4-digit numeric string.");
        }

        // Convert: First 3 digits are whole inches, last digit is tenths of an inch
        double inches = numericValue / 10.0;

        // Return as formatted string with inch symbol
        return $"{inches:0.0}\"";
    }

    public static decimal ConvertToInchesDecimal(string beltWidthCode)
    {
        if (string.IsNullOrWhiteSpace(beltWidthCode) || beltWidthCode.Length != 4 || !int.TryParse(beltWidthCode, out int numericValue))
        {
            throw new ArgumentException("Invalid belt width code. It must be a 4-digit numeric string.");
        }

        // Convert: First 3 digits are whole inches, last digit is tenths of an inch
        decimal inches = numericValue / 10.0m;

        // Round to 2 decimal places, remove trailing zeros
        return decimal.Round(inches, 2).Normalize();
    }


}

public static class DecimalExtensions
{
    public static decimal Normalize(this decimal value)
    {
        return value / 1.000000000000000000000000000000000m;
    }
}
