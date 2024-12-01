using System.Collections.Immutable;

namespace DNG.Library.Models;

public sealed record Row(
    int Index,
    ImmutableList<PartInstance> Parts,
    ImmutableList<int> SeamPositions,
    bool IsFlightRow)
{
    public int TotalLength => Parts.Sum(p => p.Length);

    public string GetRowReport()
    {
        var partDescriptions = Parts.Select(p => $"{p.Length}{(p.IsCut ? "C" : "")}");
        return $"Row{Index + 1}: {string.Join(", ", partDescriptions)}{(IsFlightRow ? " (Flight Row)" : "")}";
    }
}