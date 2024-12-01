using System.Collections.Immutable;

namespace DNG.Library.Models;

public sealed record BeltLayout(
    int BeltWidth,
    int NumberOfRows,
    bool FlightRequired,
    ImmutableList<Part> AvailableParts,
    ImmutableList<Row> Rows,
    ImmutableHashSet<string> Warnings);