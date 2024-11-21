using DNG.Library.Models;
using System.Collections.Immutable;

namespace DNG.Library.Services.Base;

public interface IBeltDesignerService
{
    BeltLayout GenerateBeltLayout(int beltWidth, int numberOfRows, bool flightRequired, ImmutableList<Part> availableParts);
}