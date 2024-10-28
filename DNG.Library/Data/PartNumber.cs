using System.ComponentModel.DataAnnotations;

namespace Client.Data
{
    public class PartNumber
    {
        [Key]
        public string Part { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}