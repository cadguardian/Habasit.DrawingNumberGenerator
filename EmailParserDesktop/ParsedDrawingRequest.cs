using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailParserDesktop;

public class ParsedDrawingRequest
{
    public string Family { get; set; } = string.Empty;
    public string Material { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Width { get; set; } = string.Empty;
    public string LinkCount { get; set; } = string.Empty;
    public string FlightOrGripInfo { get; set; } = string.Empty;
    public string RodMaterial { get; set; } = string.Empty;
    public string GeneralComments { get; set; } = string.Empty;
    public string DrawingNumber { get; set; } = string.Empty;

    // Additional parameters
    public string Subject { get; set; } = string.Empty;

    public string AssignedTo { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public string DueDate { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string SpecialBuild { get; set; } = string.Empty;
    public string TaskLink { get; set; } = string.Empty;
}