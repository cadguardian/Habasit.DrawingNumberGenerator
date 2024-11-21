using System.Collections.Immutable;
using CADCleanser.Models;

namespace CADCleanser.Services
{
    public interface IDwgProcessingService
    {
        ImmutableList<string> ExtractBlocks(string filePath);
    }
}