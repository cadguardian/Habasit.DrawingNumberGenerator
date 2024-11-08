// Report Header and File Data Classes
using System.Text.Json.Serialization;

public class ReportHeader
{
    public int TotalFiles { get; set; }

    [JsonPropertyName("total_categories")]
    public int TotalCategories { get; set; }

    [JsonPropertyName("total_size_mb")]
    public double TotalSizeMb { get; set; }

    [JsonPropertyName("generated_date")]
    public DateTime GeneratedDate { get; set; }
}