using DNG.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNG.Library.Utility
{
    public static class PartSelector
    {
        public static PartInstance SelectPart(
            int segmentLength,
            ImmutableList<Part> partsList,
            bool isFlightRow,
            int rowIndex,
            ImmutableHashSet<string>.Builder warnings)
        {
            // Try to find an exact standard part match
            var exactPart = partsList.FirstOrDefault(p => p.Length == segmentLength);
            if (exactPart != null)
            {
                return new PartInstance(Part: exactPart, Length: exactPart.Length, IsCut: false);
            }

            // If no exact match, try to find the smallest larger part to cut
            var largerParts = partsList
                .Where(p => p.Length > segmentLength)
                .OrderBy(p => p.Length)
                .ToImmutableList();

            if (largerParts.Any())
            {
                // Choose the part closest in size to the required length to minimize waste
                var partToCut = largerParts.FirstOrDefault(p => p.Length >= segmentLength * 2) ?? largerParts.First();

                warnings.Add($"WARNING: Row{rowIndex + 1}: Used cut part {partToCut.Name}C with {segmentLength} links.");

                return new PartInstance(Part: partToCut, Length: segmentLength, IsCut: true);
            }

            return null;
        }
    }
}