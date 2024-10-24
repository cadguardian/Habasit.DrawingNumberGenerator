using System.ComponentModel.DataAnnotations;

namespace Client.Data
{
    public class PartNumber
    {
        [Key]
        public string Part { get; set; }

        public string Description { get; set; }
    }
}