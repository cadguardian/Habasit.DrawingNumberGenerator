// See https://aka.ms/new-console-template for more information
using System.Collections.Immutable;

WelcomeScreen.ShowWelcomeMessage();

// Retrieve the belt layout service via dependency injection
IBeltLayoutService layoutService = new BeltLayoutService();

// Create a belt layout request with specific dimensions and settings
BeltLayoutRequest layoutRequest = layoutService.GetUserBeltLayoutRequest();

// Define a set of modules available for the layout
var modules = ImmutableArray.Create(
    new ModuleSpec(ModuleType.Middle, 24, 24, false, "PN24", "24-Link Module"),
    new ModuleSpec(ModuleType.Middle, 18, 18, false, "PN18", "18-Link Module"),
    new ModuleSpec(ModuleType.Middle, 6, 6, false, "PN06", "6-Link Module")
);

// Generate the belt layout
var result = layoutService.GenerateBeltLayout(layoutRequest, modules);

// Output the results for each module position
Console.WriteLine("Module Positions:");
foreach (var position in result.ModulePositions)
{
    Console.WriteLine($"Position: {position.PositionTag}, Type: {position.ModuleType}, "
                      + $"Links: {position.LinkCount}, Coordinates: {position.Coordinates}");
}

// Display any modules requiring cuts
Console.WriteLine("\nCut Modules:");
foreach (var cutModule in result.CutModules)
{
    Console.WriteLine($"{cutModule.PartNumber} ({cutModule.Description}), Length: {cutModule.Length}");
}

// Output any warnings generated during layout generation
if (result.Warnings.Any())
{
    Console.WriteLine("\nWarnings:");
    foreach (var warning in result.Warnings)
    {
        Console.WriteLine($"- {warning}");
    }
}

// Display the Bill of Materials (BOM) for the layout
Console.WriteLine("\nBill of Materials:");
foreach (var item in result.BillOfMaterials)
{
    Console.WriteLine($"{item.PartNumber} ({item.Description}): {item.Quantity}");
}