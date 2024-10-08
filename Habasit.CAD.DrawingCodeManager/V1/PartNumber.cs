using Habasit.CAD.DrawingCodeManager.V1.Models;
using System.Drawing;

namespace Habasit.CAD.DrawingCodeManager.V1;

public class PartNumber
{
    public string ProductType { get; set; }           // e.g., "TIS"
    public Dimension Dimension { get; set; }          // Size/dimension codes
    public Material Material { get; set; }            // Material of the belt, rods, etc.
    public MaterialColor Color { get; set; }                  // Color code
    public BeltSeries BeltSeries { get; set; }        // Series type for the belt
    public RodMaterial RodMaterial { get; set; }      // Rod material used in belt
    public List<Accessory> Accessories { get; set; }  // Belt accessories like Spiral Belt, Hold Down Tabs
    public List<Flight> Flights { get; set; }         // Flight types like curved, straight
    public SpecialFeature SpecialFeature { get; set; } // Resistance, UV, Chlorine, etc.
    public string UniqueIdentifier { get; set; }      // Custom ID for the belt (if needed)

    public PartNumber()
    {
        Accessories = new List<Accessory>();
        Flights = new List<Flight>();
    }

    // Method to generate the part number based on the input properties
    public string GeneratePartNumber()
    {
        // Basic parts concatenation with other class methods to handle specific logic
        string partNumber = $"{ProductType}{Dimension.GetCode()}{Material.Code}{Color.Code}{BeltSeries.Code}";
        partNumber += RodMaterial.Code; // Add Rod Material
        partNumber += SpecialFeature?.Code ?? ""; // Add special feature if available
        partNumber += string.Join("", Accessories.Select(a => a.Code)); // Add all accessories
        partNumber += string.Join("", Flights.Select(f => f.Code)); // Add all flights
        partNumber += UniqueIdentifier; // Add Unique ID if any

        return partNumber;
    }
}

