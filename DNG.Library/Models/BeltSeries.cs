using System.Xml.Linq;

namespace DNG.Library.Models;

public class BeltSeries : RuleWithOptions, IOptions
{
    public BeltSeries(string name, string code) : base(
        name,
        code,
        maxCharacters: code.Length)
    { }

    public static Dictionary<string, string> Options => new Dictionary<string, string>()
    {
        // KVP Belt Series
        ["SM605"] = "KVP",
        ["CM605"] = "KVP",
        ["HDS605FT"] = "KVP",
        ["HDS05TT"] = "KVP",
        ["106-10"] = "KVP",
        ["106-22"] = "KVP",
        ["106-FT"] = "KVP",
        ["106-RT"] = "KVP",
        ["106-V"] = "KVP",
        ["IS610"] = "KVP",
        ["ST610"] = "KVP",
        ["VT610"] = "KVP",
        ["CVT610"] = "KVP",
        ["HDS610"] = "KVP",
        ["208-35"] = "KVP",
        ["208-FT"] = "KVP",
        ["208-RR"] = "KVP",
        ["MB610"] = "KVP",
        ["MB610MTW"] = "KVP",
        ["F51"] = "KVP",
        ["F52"] = "KVP",
        ["F52SF"] = "KVP",
        ["F53"] = "KVP",
        ["F54"] = "KVP",
        ["PR61216"] = "KVP",
        ["PR61222"] = "KVP",
        ["SP615"] = "KVP",
        ["IS615"] = "KVP",
        ["ST615"] = "KVP",
        ["VT615"] = "KVP",
        ["CC41"] = "KVP",
        ["CC42"] = "KVP",
        ["SP620"] = "KVP",
        ["SE620"] = "KVP",
        ["IS620"] = "KVP",
        ["HDS620FT"] = "KVP",
        ["HDS620CT"] = "KVP",
        ["HDS620VT"] = "KVP",
        ["HDS620EZR"] = "KVP",
        ["HDU620FT"] = "KVP",
        ["HDU620CT"] = "KVP",
        ["HDU620VT"] = "KVP",
        ["HDU620EZR"] = "KVP",
        ["FF620"] = "KVP",
        ["FF620WR"] = "KVP",
        ["FF620MC"] = "KVP",
        ["MB620"] = "KVP",
        ["MB620VT"] = "KVP",
        ["MB620TT"] = "KVP",
        ["PR620"] = "KVP",
        ["PR620SPS"] = "KVP",
        ["PR620SPSCT"] = "KVP",
        ["PR620TTR"] = "KVP",

        // Habasit Link Belt Series
        ["870"] = "HabasitLink",
        ["873"] = "HabasitLink",
        ["1185"] = "HabasitLink",
        ["1220"] = "HabasitLink",
        ["1233"] = "HabasitLink",
        ["1234"] = "HabasitLink",
        ["1280"] = "HabasitLink",
        ["2470"] = "HabasitLink",
        ["2472"] = "HabasitLink",
        ["2480"] = "HabasitLink",
        ["2510"] = "HabasitLink",
        ["2511"] = "HabasitLink",
        ["2514"] = "HabasitLink",
        ["2516"] = "HabasitLink",
        ["2520"] = "HabasitLink",
        ["2527"] = "HabasitLink",
        ["2531"] = "HabasitLink",
        ["2533"] = "HabasitLink",
        ["2540"] = "HabasitLink",
        ["2543"] = "HabasitLink",
        ["2544"] = "HabasitLink",
        ["2585"] = "HabasitLink",
        ["2586"] = "HabasitLink",
        ["2620"] = "HabasitLink",
        ["2623"] = "HabasitLink",
        ["2670"] = "HabasitLink",
        ["3840"] = "HabasitLink",
        ["3843"] = "HabasitLink",
        ["3892"] = "HabasitLink",
        ["5010"] = "HabasitLink",
        ["5011"] = "HabasitLink",
        ["5012"] = "HabasitLink",
        ["5013"] = "HabasitLink",
        ["5014"] = "HabasitLink",
        ["5015"] = "HabasitLink",
        ["5020"] = "HabasitLink",
        ["5021"] = "HabasitLink",
        ["5023"] = "HabasitLink",
        ["5032"] = "HabasitLink",
        ["5033"] = "HabasitLink",
        ["5060"] = "HabasitLink",
        ["5062"] = "HabasitLink",
        ["5064"] = "HabasitLink",
        ["5067"] = "HabasitLink",
        ["5131"] = "HabasitLink",
        ["5182"] = "HabasitLink",
        ["5293"] = "HabasitLink",
        ["5493"] = "HabasitLink",
        ["5482"] = "HabasitLink",
        ["6360"] = "HabasitLink",
        ["6420"] = "HabasitLink",
        ["6423"] = "HabasitLink"
    };

    public static string GetBeltSeriesType(string code)
    {
        var type = Options.FirstOrDefault(
            kvp => kvp.Key.Equals(
                code,
                StringComparison.OrdinalIgnoreCase)
            ).Value;
        return type ?? "Unknown"; // Return "Unknown" if not found
    }

    public static BeltSeries Create(
        string name,
        string code)
    {
        return new BeltSeries(name, code);
    }

    public static BeltSeries Test()
    {
        var name = "";
        var code = "IS610";
        var series = BeltSeries.Create(name, code);
        return series;
    }

    public override string ToString()
    {
        return Code;
    }
}