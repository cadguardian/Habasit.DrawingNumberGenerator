namespace Habasit.CAD.DrawingCodeManager.V1.Models;
public class Flight
{
    public string Name { get; set; }
    public string Code { get; set; }

    // Static list of flight types and their codes
    public static List<Flight> Flights = new List<Flight>()
    {
        new Flight("2-inch Straight Flight", "F2"),
        // Add more based on your data
    };

    public Flight(string name, string code)
    {
        Name = name;
        Code = code;
    }

    // Static method to get flight by code
    public static Flight GetFlightByCode(string code)
    {
        return Flights.FirstOrDefault(f => f.Code == code);
    }
}

