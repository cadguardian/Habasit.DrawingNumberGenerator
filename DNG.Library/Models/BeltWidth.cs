using DrawingNumberGenerator.Library.Models;

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
}