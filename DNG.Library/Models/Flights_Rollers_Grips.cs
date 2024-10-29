namespace DNG.Library.Models;

// todo assign frgCentersDimension, quantityRollersAcrossWidth appropriately
// convert all code formats appropriately

public class Flights_Rollers_Grips : IOptions
{
    public string Name { get; } = string.Empty;
    public string Code { get; } = string.Empty;
    public int MaxCharacters { get; }
    public string AccessoryType { get; } = string.Empty;

    public static Dictionary<string, string> Options =>
        GetFlights()
        .Concat(GetRollers())
        .Concat(GetGripTops())
        .ToDictionary();

    public Flights_Rollers_Grips(string name,
        string FlightsRollersGripsCode,
        int quantityRollersAcrossWidth = 0,
        decimal frgCentersDimension = 0)
    {
        Name = name;
        //AccessoryType = accessoryType;
        MaxCharacters = 6;

        Code = GetFRG_Quantity_Centers_Code(name,
            quantityRollersAcrossWidth,
            frgCentersDimension);
    }

    private static Dictionary<string, string> GetOptionsByType(string type)
    {
        return type.ToLower() switch
        {
            "flight" => GetFlights(),
            "griptop" => GetGripTops(),
            "roller" => GetRollers(),
            _ => []
        };
    }

    private static Dictionary<string, string> GetFlights()
    {
        return new Dictionary<string, string>
        {
            { "00", "No Flights" },
            { "01", "1” Straight Flight" },
            { "02", "2” Straight Flight" },
            { "03", "3” Straight Flight" },
            { "04", "4” Straight Flight" },
            { "06", "6” Straight Flight" },
            { "07", "6” EZR Flight" },
            { "0A", "1-½” Straight Flight" },
            { "0B", "6” Bucket Flight" },
            { "0C", "4” Bucket Flight" },
            { "0D", "3” Bucket Flight" },
            { "0E", "3\" Scoop/Curved Flight" },
            { "0H", "½” Straight Flight" },
            { "0K", "4” Scoop/Curved Flight" },
            { "0L", "6” Scoop/Curved Flight" },
            { "0N", "Bent Flight–See Dwg." },
            { "0Q", "¼” Mini-Cleat" },
            { "0R", "4” Radius Base Flight" },
            { "0S", "6” Super Flight (Slide)" },
            { "0U", "3” Curve Urethane Flight" },
            { "0V", "3” Str. Urethane Flight" },
            { "0W", "4” Curve Urethane Flight" },
            { "0X", "Special Cut Flight" },
            { "0Y", "2” Str. Urethane Flight" },
            { "0Z", "4” Str. Urethane Flight" }
        };
    }

    private static Dictionary<string, string> GetGripTops()
    {
        return new Dictionary<string, string>
        {
            { "GF", "Flat Grip Top" },
            { "GH", "High Grip Top" }
        };
    }

    private static Dictionary<string, string> GetRollers()
    {
        return new Dictionary<string, string>
        {
            { "R1", ".2\" Wide Staggered" },
            { "R2", ".6\" Wide Staggered" },
            { "R3", ".2\" Wide Inline" },
            { "R4", ".6\" Wide Inline" }
        };
    }

    public static string GetAccessoryTypeByName(string name)
    {
        if (GetFlights().ContainsValue(name)) return "Flight";
        if (GetGripTops().ContainsValue(name)) return "Grip Top";
        if (GetRollers().ContainsValue(name)) return "Roller";
        return "Unknown";
    }

    public static string GetNameByCode(string flightsRollersGripsCode)
    {
        string codeUpper = flightsRollersGripsCode.ToUpper();
        return Options.TryGetValue(codeUpper, out var name) ? name : "Unknown";
    }

    public static string GetFlightsRollersGripsCode(string flightsRollersGripsName)
    {
        return Options.FirstOrDefault(kvp => kvp.Value.Equals(flightsRollersGripsName, StringComparison.OrdinalIgnoreCase)).Key ?? "Unknown";
    }

    public static string GetAccessoryTypeByCode(string code)
    {
        string codeUpperCase = code.ToUpper();
        if (GetFlights().ContainsKey(codeUpperCase)) return "Flight";
        if (GetGripTops().ContainsKey(codeUpperCase)) return "Grip Top";
        if (GetRollers().ContainsKey(codeUpperCase)) return "Roller";
        return "Unknown";
    }

    public static string GetFRG_Quantity_Centers_Code(string name,
        int quantityRollersAcrossWidth = 0,
        decimal frgCentersDimension = 0)
    {
        return $"{RuleWithOptions.GetCodeByName(name, Options)}" +
            $"{GetFRGQuantityAcrossWidthCode(quantityRollersAcrossWidth)}" +
            $"{GetFRGCentersCode(frgCentersDimension)}";
    }

    public static string GetFRGCentersCode(decimal frgCentersDimension)
    {
        // Get the integer part and the decimal part
        int integerPart = (int)frgCentersDimension;
        int tensInt = integerPart / 10;
        int onesInt = integerPart % 10;

        // Extract the first decimal digit
        int decimalInt = (int)((frgCentersDimension - integerPart) * 10) % 10;

        return $"{tensInt}{onesInt}{decimalInt}";
    }


    public static string GetFRGQuantityAcrossWidthCode(int quantity)
    {
        return FormatQuantityDecimal(quantity);
    }

    private static string FormatQuantityDecimal(int quantity)
    {
        return $"{quantity:00}"; // Formats the integer to always have 2 digits
    }
}