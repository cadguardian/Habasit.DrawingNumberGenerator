using System.Collections.Immutable;

public record BeltLayoutRequest(
    string Series,         // Identifier for belt series (e.g., "M5060")
    double Width,             // Total width of the belt in links
    double Height,            // Belt height in inches (center-to-center)
    double Pitch,             // Vertical pitch between rows in inches
    bool RequiresEdges     // True if edge modules are required
);

public record ModuleSpec(
    ModuleType Type,       // Enum for module type (LeftEdge, Middle, RightEdge)
    int LinkCount,         // Number of links in this module
    double Length,         // Physical length of module in inches
    bool RequiresCut,      // True if this module requires a cut
    string PartNumber,     // Part number, used in BOM
    string Description     // Description of the module
);

public record ModulePosition(
    string PositionTag,    // Tag for module's position (e.g., "1.1")
    ModuleType ModuleType, // Enum indicating the module type
    int LinkCount,         // Number of links in this module
    (double X, double Y) Coordinates // Position in the layout (X, Y)
);

public record BeltLayoutResult(
    ImmutableArray<ModulePosition> ModulePositions, // All module positions
    ImmutableArray<ModuleSpec> CutModules,          // Modules requiring cuts
    ImmutableArray<string> Warnings,                // Warnings from layout
    ImmutableArray<BOMItem> BillOfMaterials         // BOM for the layout
);

public record BOMItem(
    string PartNumber,     // Unique part number for BOM entry
    string Description,    // Detailed description of the module
    int Quantity           // Quantity of each part used in layout
);

public enum ModuleType
{
    LeftEdge,              // Module that appears on the left edge
    Middle,                // Default type for center modules
    RightEdge              // Module that appears on the right edge
}