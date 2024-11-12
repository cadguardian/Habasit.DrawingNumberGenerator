using System.Collections.Immutable;

public class BeltLayoutService : IBeltLayoutService
{
    public BeltLayoutService()
    { }

    public BeltLayoutResult GenerateBeltLayout(
        BeltLayoutRequest layoutRequest,
        ImmutableArray<ModuleSpec> modules)
    {
        var modulePositions = ImmutableArray.CreateBuilder<ModulePosition>();
        var cutModules = ImmutableArray.CreateBuilder<ModuleSpec>();
        var warnings = ImmutableArray.CreateBuilder<string>();
        var billOfMaterials = new Dictionary<string, BOMItem>();

        int rowCount = (int)(layoutRequest.Height / layoutRequest.Pitch);
        var filteredModules = FilterModulesByWidth(layoutRequest.Width, modules);

        if (filteredModules.IsEmpty)
        {
            warnings.Add("No suitable modules found within width constraints.");
            return new BeltLayoutResult(
                modulePositions.ToImmutable(),
                cutModules.ToImmutable(),
                warnings.ToImmutable(),
                billOfMaterials.Values.ToImmutableArray()
            );
        }

        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            double xPosition = 0;
            double yPosition = rowIndex * layoutRequest.Pitch;

            while (xPosition < layoutRequest.Width)
            {
                var module = SelectBestFitModule(layoutRequest.Width - xPosition,
                                                 filteredModules, rowIndex,
                                                 modulePositions);

                if (module == null)
                {
                    warnings.Add($"No suitable module for row {rowIndex + 1} at X: {xPosition}");
                    break;
                }

                bool requiresCut = (xPosition + module.Length) > layoutRequest.Width;
                if (requiresCut)
                {
                    module = ApplyInsideCut(module, layoutRequest.Width - xPosition, cutModules);
                }

                var positionTag = $"{rowIndex + 1}.{modulePositions.Count(m => m.Coordinates.Y == yPosition) + 1}";
                modulePositions.Add(new ModulePosition(
                    PositionTag: positionTag,
                    ModuleType: module.Type,
                    LinkCount: module.LinkCount,
                    Coordinates: (xPosition, yPosition)
                ));

                xPosition += module.Length;
                AddToBillOfMaterials(billOfMaterials, module);
            }
        }

        return new BeltLayoutResult(
            ModulePositions: modulePositions.ToImmutable(),
            CutModules: cutModules.ToImmutable(),
            Warnings: warnings.ToImmutable(),
            BillOfMaterials: billOfMaterials.Values.ToImmutableArray()
        );
    }

    private ImmutableArray<ModuleSpec> FilterModulesByWidth(double beltWidth, ImmutableArray<ModuleSpec> modules)
    {
        return modules
            .Where(m => m.Length <= beltWidth)
            .OrderByDescending(m => m.Length)
            .ToImmutableArray();
    }

    private ModuleSpec? SelectBestFitModule(double remainingWidth, ImmutableArray<ModuleSpec> modules,
                                            int rowIndex, ImmutableArray<ModulePosition>.Builder modulePositions)
    {
        return modules.FirstOrDefault(module =>
            module.Length <= remainingWidth &&
            IsVerticalOffsetValid(module, rowIndex, modulePositions)
        );
    }

    private bool IsVerticalOffsetValid(ModuleSpec module, int rowIndex,
                                       ImmutableArray<ModulePosition>.Builder modulePositions)
    {
        var sameLinkModulesAbove = modulePositions.Where(pos =>
            pos.LinkCount == module.LinkCount &&
            Math.Abs(pos.Coordinates.Y - rowIndex * module.Length) <= 2
        );

        return !sameLinkModulesAbove.Any();
    }

    private ModuleSpec ApplyInsideCut(ModuleSpec module, double targetLength,
                                      ImmutableArray<ModuleSpec>.Builder cutModules)
    {
        double cutLength = module.Length - targetLength;
        if (cutLength < 2)
        {
            throw new InvalidOperationException("Cut length must be at least 2 links.");
        }

        var cutModule = module with { Length = targetLength, RequiresCut = true };
        cutModules.Add(cutModule);

        return cutModule;
    }

    private void AddToBillOfMaterials(Dictionary<string, BOMItem> billOfMaterials, ModuleSpec module)
    {
        if (billOfMaterials.TryGetValue(module.PartNumber, out var existingItem))
        {
            billOfMaterials[module.PartNumber] = existingItem with
            { Quantity = existingItem.Quantity + 1 };
        }
        else
        {
            billOfMaterials[module.PartNumber] = new BOMItem(module.PartNumber, module.Description, 1);
        }
    }

    public BeltLayoutRequest GetUserBeltLayoutRequest()
    {
        Console.WriteLine("Please enter the following details for the belt layout request.");
        Console.WriteLine("Type 'default' to use the recommended default values.\n");

        // Default values
        const string defaultSeries = "CC42";
        const float defaultWidth = 11.9f;
        const float defaultHeight = 8.75f;
        const float defaultPitch = 1.75f;
        const bool defaultRequiresEdges = true;

        // Function to get a float with error handling and default option
        float GetValidFloat(string prompt, float defaultValue)
        {
            while (true)
            {
                Console.Write($"{prompt} (or 'default' for {defaultValue}): ");
                string input = Console.ReadLine()?.Trim().ToLower();

                if (input == "default")
                    return defaultValue;

                if (float.TryParse(input, out float result) && result > 0)
                    return result;

                Console.WriteLine("Invalid input. Please enter a positive decimal number.");
            }
        }

        // Function to get a boolean with error handling and default option
        bool GetValidBool(string prompt, bool defaultValue)
        {
            while (true)
            {
                Console.Write($"{prompt} (true/false or 'default' for {defaultValue}): ");
                string input = Console.ReadLine()?.Trim().ToLower();

                if (input == "default")
                    return defaultValue;

                if (bool.TryParse(input, out bool result))
                    return result;

                Console.WriteLine("Invalid input. Please enter 'true' or 'false'.");
            }
        }

        // Prompt user for each required input with default options
        Console.Write($"Enter Belt Series (or 'default' for {defaultSeries}): ");
        string series = Console.ReadLine()?.Trim();
        if (series?.ToLower() == "default" || string.IsNullOrEmpty(series))
            series = defaultSeries;

        float width = GetValidFloat("Enter Belt Width (in links)", defaultWidth);
        float height = GetValidFloat("Enter Belt Height (in inches)", defaultHeight);
        float pitch = GetValidFloat("Enter Pitch per Row (in inches)", defaultPitch);
        bool requiresEdges = GetValidBool("Does the layout require edge modules?", defaultRequiresEdges);

        Console.WriteLine("\nBelt Layout Request Created Successfully:");
        Console.WriteLine($"Series: {series}");
        Console.WriteLine($"Width: {width} links");
        Console.WriteLine($"Height: {height} inches");
        Console.WriteLine($"Pitch: {pitch} inches");
        Console.WriteLine($"Requires Edges: {requiresEdges}");

        // Create and return the BeltLayoutRequest object with user inputs or defaults
        return new BeltLayoutRequest(
            Series: series,
            Width: width,
            Height: height,
            Pitch: pitch,
            RequiresEdges: requiresEdges
        );
    }
}