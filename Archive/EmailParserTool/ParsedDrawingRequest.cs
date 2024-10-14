using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailParserTool;

public class ParsedDrawingRequest
{
    public string Family { get; set; }
    public string Material { get; set; }
    public string Color { get; set; }
    public string Width { get; set; }
    public string LinkCount { get; set; }
    public string FlightOrGripInfo { get; set; }
    public string RodMaterial { get; set; }
    public string GeneralComments { get; set; }
    public string DrawingNumber { get; set; }

    // Additional parameters
    public string Subject { get; set; }
    public string AssignedTo { get; set; }
    public string Priority { get; set; }
    public string DueDate { get; set; }
    public string Status { get; set; }
    public string SpecialBuild { get; set; }
    public string TaskLink { get; set; }
}

