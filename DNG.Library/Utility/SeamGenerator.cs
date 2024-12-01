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
            // For flight rows, calculate evenly spaced seams for symmetry
            return GenerateSymmetricSeams(beltWidth, spacing: 8);
        }

        // Alternate seam placements to avoid vertical alignment
        return rowIndex % 2 == 0
            ? GenerateDynamicSeams(beltWidth, start: 8, spacing: 12)
            : GenerateDynamicSeams(beltWidth, start: 12, spacing: 12);
    }

    private static ImmutableList<int> GenerateSymmetricSeams(int beltWidth, int spacing)
    {
        // Generate evenly spaced seam positions based on the given spacing
        return ImmutableList.CreateRange(
            Enumerable.Range(1, beltWidth / spacing).Select(i => i * spacing).Where(pos => pos < beltWidth)
        );
    }

    private static ImmutableList<int> GenerateDynamicSeams(int beltWidth, int start, int spacing)
    {
        // Generate dynamic seam positions starting at start with the specified spacing
        return ImmutableList.CreateRange(
            Enumerable.Range(0, beltWidth / spacing + 1).Select(i => start + i * spacing).Where(pos => pos < beltWidth)
        );
    }
}