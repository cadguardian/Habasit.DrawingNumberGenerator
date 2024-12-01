using System.Collections.Immutable;

namespace DNG.Library.Utility;

public static class SeamGenerator
{
    public static ImmutableList<int> GenerateSeamPositions(
    int beltWidth,
    int rowIndex,
    ImmutableList<int> previousRowSeams,
    bool isFlightRow)
    {
        if (isFlightRow)
        {
            return ImmutableList.CreateRange(new[] { 5, 13, 21, 29 }.Where(s => s < beltWidth));
        }

        // Base seam positions
        var proposedSeams = rowIndex % 2 == 0
            ? ImmutableList.CreateRange(new[] { 8, 20, 32 }.Where(s => s < beltWidth))
            : ImmutableList.CreateRange(new[] { 12, 24 }.Where(s => s < beltWidth));

        // Adjust to prevent violations
        var adjustedSeams = ImmutableList.CreateBuilder<int>();
        foreach (var seam in proposedSeams)
        {
            if (previousRowSeams.All(prevSeam => Math.Abs(prevSeam - seam) >= 2))
            {
                adjustedSeams.Add(seam);
            }
        }

        // Ensure at least one valid seam remains
        //if (adjustedSeams.Count == 0)
        //{
        //    throw new Exception($"No valid seam positions available for row {rowIndex + 1}.");
        //}

        return adjustedSeams.ToImmutable();
    }
}