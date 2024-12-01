using DNG.Library.Models;
using System.Collections.Immutable;

namespace DNG.Library.Utility;

public static class PartSelector
{
    public static PartInstance SelectPart(
    int segmentLength,
    ImmutableList<Part> partsList,
    bool isFlightRow,
    int rowIndex,
    ImmutableHashSet<string>.Builder warnings)
    {
        // Exclude 1-link modules entirely
        var validParts = partsList.Where(p => p.Length > 1).ToImmutableList();

        // Try to find an exact standard part match
        var exactPart = validParts.FirstOrDefault(p => p.Length == segmentLength);
        if (exactPart != null)
        {
            return new PartInstance(Part: exactPart, Length: exactPart.Length, IsCut: false);
        }

        // If no exact match, find the smallest larger part to cut
        var largerParts = validParts
            .Where(p => p.Length > segmentLength)
            .OrderBy(p => p.Length)
            .ToImmutableList();

        if (largerParts.Any())
        {
            var partToCut = largerParts.First();
            warnings.Add($"Row{rowIndex + 1}: Cut {partToCut.Name} to {segmentLength} links.");
            return new PartInstance(Part: partToCut, Length: segmentLength, IsCut: true);
        }

        throw new Exception($"No valid parts available for segment length {segmentLength} in row {rowIndex + 1}.");
    }
}