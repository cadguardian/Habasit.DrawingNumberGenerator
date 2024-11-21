using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // For flight rows, use predefined seams for symmetry
            return ImmutableList.CreateRange(new[] { 5, 13, 21, 29 }.Where(s => s < beltWidth));
        }

        // Define seam positions to avoid vertical alignment
        var seamPositions = rowIndex % 2 == 0
            ? ImmutableList.CreateRange(new[] { 8, 20, 32 }.Where(s => s < beltWidth))
            : ImmutableList.CreateRange(new[] { 12, 24 }.Where(s => s < beltWidth));

        return seamPositions;
    }
}