using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNG.Library.Models
{
    public sealed record BeltLayout(
        int BeltWidth,
        int NumberOfRows,
        bool FlightRequired,
        ImmutableList<Part> AvailableParts,
        ImmutableList<Row> Rows,
        ImmutableHashSet<string> Warnings);
}