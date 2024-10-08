using System.Collections.Generic;

namespace Habasit.CAD.DrawingCodeManager.V1.Models;
public class Accessory : Category
{

    // Static list of accessories and their codes
    public static new List<Accessory> Options = new List<Accessory>()
    {
        new Accessory("Spiral Belt", "SB"),
        // Add more based on your data
    };

    public Accessory(string name, string code)
    {
        Name = name;
        Code = code;
    }
}
