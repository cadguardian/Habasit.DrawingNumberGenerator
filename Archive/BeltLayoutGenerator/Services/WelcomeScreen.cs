public static class WelcomeScreen
{
    public static void ShowWelcomeMessage()
    {
        Console.WriteLine("==========================================");
        Console.WriteLine("      Welcome to the Modular Belt Layout Generator!");
        Console.WriteLine("==========================================\n");

        Console.WriteLine("Purpose:");
        Console.WriteLine("This app automates the design of precise, interlocking");
        Console.WriteLine("modular belt layouts, optimizing part usage and ensuring");
        Console.WriteLine("maximum strength and efficiency.\n");

        Console.WriteLine("How to Use:");
        Console.WriteLine("1. Enter Belt Specifications");
        Console.WriteLine("   - Input the width, height, pitch, and whether edge");
        Console.WriteLine("     modules are required.");
        Console.WriteLine("2. Select Available Modules");
        Console.WriteLine("   - Add modules by defining their length, link count,");
        Console.WriteLine("     and type (Left Edge, Middle, Right Edge).");
        Console.WriteLine("3. Generate Layout");
        Console.WriteLine("   - Click 'Generate Layout' to visualize your optimized");
        Console.WriteLine("     belt assembly.");
        Console.WriteLine("4. Review and Export");
        Console.WriteLine("   - View each module’s position, any cut requirements,");
        Console.WriteLine("     and a Bill of Materials (BOM) for easy ordering.");
        Console.WriteLine("   - Export or print the layout for production.\n");

        Console.WriteLine("Key Features:");
        Console.WriteLine("- Automatic Bricklay Pattern: Ensures seams don’t align");
        Console.WriteLine("  vertically, maintaining belt strength.");
        Console.WriteLine("- Intelligent Cut Handling: Cuts are made from the inside,");
        Console.WriteLine("  with symmetrical edges for a clean finish.");
        Console.WriteLine("- Detailed BOM: Instantly track parts and quantities for");
        Console.WriteLine("  hassle-free assembly.\n");

        Console.WriteLine("Start building your belt layout with precision and efficiency.");
        Console.WriteLine("Let’s get started!\n");
        Console.WriteLine("==========================================");
    }
}