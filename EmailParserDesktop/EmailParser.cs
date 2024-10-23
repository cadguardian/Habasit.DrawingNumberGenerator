using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailParserDesktop;

public class EmailParser
{
    public static EmailParser CreateEmailParser()
    {
        return new EmailParser();
    }

    public ParsedDrawingRequest ParseEmail(string emailBody)
    {
        var lines = emailBody.Split('\n');
        var parsedRequest = new ParsedDrawingRequest();

        // Extract Subject
        var subjectLine = lines.FirstOrDefault(l => l.StartsWith("Subject:"));
        if (subjectLine != null)
        {
            parsedRequest.Subject = subjectLine.Replace("Subject:", "").Trim();
        }

        // Extract Assigned User
        var assignedLine = lines.FirstOrDefault(l => l.Contains("has assigned a Task"));
        if (assignedLine != null)
        {
            parsedRequest.AssignedTo = assignedLine.Split(' ')[0]; // Example to extract "Williams"
        }

        // Extract Priority
        var priorityLine = lines.FirstOrDefault(l => l.StartsWith("Priority:"));
        if (priorityLine != null)
        {
            parsedRequest.Priority = priorityLine.Replace("Priority:", "").Trim();
        }

        // Extract Due Date
        var dueDateLine = lines.FirstOrDefault(l => l.StartsWith("Due Date:"));
        if (dueDateLine != null)
        {
            parsedRequest.DueDate = dueDateLine.Replace("Due Date:", "").Trim();
        }

        // Extract Task Status
        var statusLine = lines.FirstOrDefault(l => l.StartsWith("Status:"));
        if (statusLine != null)
        {
            parsedRequest.Status = statusLine.Replace("Status:", "").Trim();
        }

        // Extract Line 1: Family, Material, Color, Width, LinkCount
        var beltLine = lines.FirstOrDefault(l => l.StartsWith("BELT:"));
        if (beltLine != null)
        {
            var beltParts = beltLine.Replace("BELT:", "").Trim().Split(' ');
            parsedRequest.Family = beltParts[0]; // IS610
            parsedRequest.Material = beltParts[^2]; // Polypropylene
            parsedRequest.Color = beltParts[^1]; // White
        }

        // Extract Line 2: Flight or Grip Info
        var flightOrGripLine = lines.FirstOrDefault(l => l.StartsWith("HOLDDOWN:"));
        if (flightOrGripLine != null)
        {
            parsedRequest.FlightOrGripInfo = flightOrGripLine.Replace("HOLDDOWN:", "").Trim();
        }

        // Extract Line 3: Indent and Rod Info
        var rodLine = lines.FirstOrDefault(l => l.StartsWith("NonStd RODS:"));
        if (rodLine != null)
        {
            parsedRequest.RodMaterial = rodLine.Replace("NonStd RODS:", "").Trim();
        }

        // Extract Belt Width and LinkCount
        var widthLine = lines.FirstOrDefault(l => l.StartsWith("BELT WIDTH:"));
        if (widthLine != null)
        {
            var widthParts = widthLine.Replace("BELT WIDTH:", "").Split(' ');
            parsedRequest.Width = widthParts[0]; // "8.0 IN"
            parsedRequest.LinkCount = widthParts[^2]; // "(15) Links"
        }

        // Extract Special Build Note
        var specialBuildLine = lines.FirstOrDefault(l => l.Contains("*** SPECIAL BUILD"));
        if (specialBuildLine != null)
        {
            parsedRequest.SpecialBuild = specialBuildLine.Replace("*** SPECIAL BUILD :", "").Trim();
        }

        // Extract General Comments
        var generalCommentsLine = lines.FirstOrDefault(l => l.StartsWith("GENERAL COMMENTS:"));
        if (generalCommentsLine != null)
        {
            parsedRequest.GeneralComments = generalCommentsLine.Replace("GENERAL COMMENTS:", "").Trim();
        }

        // Extract Task Link
        var taskLinkLine = lines.FirstOrDefault(l => l.StartsWith("You may review this Task at:"));
        if (taskLinkLine != null)
        {
            parsedRequest.TaskLink = taskLinkLine.Replace("You may review this Task at:", "").Trim();
        }

        // Generate a drawing number
        parsedRequest.DrawingNumber = GenerateDrawingNumber(parsedRequest);

        return parsedRequest;
    }

    private string GenerateDrawingNumber(ParsedDrawingRequest request)
    {
        return $"{request.Family}-{request.Material}-{DateTime.Now:yyyyMMddHHmmss}";
    }
}

