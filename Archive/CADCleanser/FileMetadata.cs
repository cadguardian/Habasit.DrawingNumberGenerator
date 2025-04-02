using System;
using System.Collections.Immutable;

namespace CADCleanser.Models
{
    public record FileMetadata(
        string Filename,
        string RelativePath,
        double SizeMb,
        DateTime CreatedDate
    );
}