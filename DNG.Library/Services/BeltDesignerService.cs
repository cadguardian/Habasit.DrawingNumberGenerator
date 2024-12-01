using DNG.Library.Models;
using DNG.Library.Services.Base;
using DNG.Library.Utility;
using System.Collections.Immutable;

namespace DNG.Library.Services;

public class BeltDesignerService : IBeltDesignerService
{
    public BeltLayout GenerateBeltLayout(int beltWidth, int numberOfRows, bool flightRequired, ImmutableList<Part> availableParts)
    {
        var rows = ImmutableList.CreateBuilder<Row>();
        var warnings = ImmutableHashSet.CreateBuilder<string>();
        var previousRowSeams = ImmutableList<int>.Empty;

        for (int i = 0; i < numberOfRows; i++)
        {
            bool isFlightRow = flightRequired && (i == 0 || i == numberOfRows - 1);
            var seamPositions = SeamGenerator.GenerateSeamPositions(beltWidth, i, previousRowSeams, isFlightRow);

            var row = BuildRow(
                beltWidth: beltWidth,
                rowIndex: i,
                seamPositions: seamPositions,
                isFlightRow: isFlightRow,
                availableParts: availableParts,
                warnings: warnings);

            rows.Add(row);
            previousRowSeams = seamPositions;
        }

        return new BeltLayout(
            BeltWidth: beltWidth,
            NumberOfRows: numberOfRows,
            FlightRequired: flightRequired,
            AvailableParts: availableParts,
            Rows: rows.ToImmutable(),
            Warnings: warnings.ToImmutable());
    }

    private static Row BuildRow(
        int beltWidth,
        int rowIndex,
        ImmutableList<int> seamPositions,
        bool isFlightRow,
        ImmutableList<Part> availableParts,
        ImmutableHashSet<string>.Builder warnings)
    {
        var parts = ImmutableList.CreateBuilder<PartInstance>();
        int currentPosition = 0;

        var partsList = availableParts
            .Where(p => p.Type == (isFlightRow ? PartType.Flight : PartType.Module))
            .ToImmutableList();

        var extendedSeamPositions = seamPositions.Add(beltWidth);

        foreach (var seamPosition in extendedSeamPositions)
        {
            int segmentLength = seamPosition - currentPosition;

            var partInstance = PartSelector.SelectPart(segmentLength, partsList, isFlightRow, rowIndex, warnings);

            if (partInstance == null)
            {
                throw new Exception($"Unable to find suitable part for row {rowIndex + 1}, segment starting at position {currentPosition} with length {segmentLength}.");
            }

            parts.Add(partInstance);
            currentPosition += partInstance.Length;
        }

        return new Row(
            Index: rowIndex,
            Parts: parts.ToImmutable(),
            SeamPositions: seamPositions,
            IsFlightRow: isFlightRow);
    }
}