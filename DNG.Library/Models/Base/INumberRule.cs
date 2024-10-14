namespace DrawingNumberGenerator.Library.Models;

public interface INumberRule
{
    public int TensPlace { get; }
    public int OnesPlace { get; }
    public int DecimalPlace { get; }

    public int CalculateTensPlace(decimal width);

    public int CalculateOnesPlace(decimal width);

    public int CalculateDecimalPlace(decimal width);
}