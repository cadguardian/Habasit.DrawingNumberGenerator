using DNG.Library.Models;
using DNG.Library.Models.Base;
using DNG.Library.Models.BeltSpecs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNG.Library.Services
{
    public class DrawingNumberDecipherService : IDrawingNumberDecipherService
    {
        private readonly ILogger<DrawingNumberDecipherService> _logger;

        public DrawingNumberDecipherService(ILogger<DrawingNumberDecipherService> logger)
        {
            _logger = logger;
        }

        public Dictionary<string, (string DrawingCode, string DrawingRequestValue)> DecipherDrawingNumber(string drawingNumber)
        {
            var result = new Dictionary<string, (string DrawingCode, string DrawingRequestValue)>();
            var metadata = DrawingNumberAttributeMetadata.GetAttributeMetadata();

            int currentIndex = 0;
            bool beltSeriesMatched = false;

            _logger.LogInformation("Starting to decipher drawing number: {DrawingNumber} at {Timestamp}", drawingNumber, DateTime.Now);

            foreach (var (attributeName, maxLength, lookupFunc) in metadata)
            {
                if (currentIndex >= drawingNumber.Length)
                {
                    _logger.LogWarning("Reached end of drawing number string before completing all attributes.");
                    break;
                }

                string codeSegment;
                string drawingRequestValue; // Declare this variable once outside the scope to avoid conflicts.

                // Special handling for BeltSeries
                if (attributeName == "BeltSeries" && !beltSeriesMatched)
                {
                    codeSegment = MatchBeltSeries(drawingNumber, currentIndex, maxLength, out int matchedLength);
                    beltSeriesMatched = true;

                    // Adjust index after BeltSeries match
                    currentIndex += matchedLength;

                    // Lookup and log
                    drawingRequestValue = lookupFunc(codeSegment);
                    result[attributeName] = (codeSegment, drawingRequestValue);
                    LogAttributeResult(attributeName, codeSegment, drawingRequestValue);
                    continue;
                }

                // Extract code segment based on fixed length
                codeSegment = drawingNumber.Substring(currentIndex, Math.Min(maxLength, drawingNumber.Length - currentIndex));
                currentIndex += maxLength;

                // Lookup and log
                drawingRequestValue = lookupFunc(codeSegment);
                result[attributeName] = (codeSegment, drawingRequestValue);
                LogAttributeResult(attributeName, codeSegment, drawingRequestValue);
            }

            _logger.LogInformation("Finished deciphering drawing number: {DrawingNumber} at {Timestamp}. Total attributes processed: {AttributeCount}.", drawingNumber, DateTime.Now, result.Count);

            return result;
        }

        private string MatchBeltSeries(string drawingNumber, int startIndex, int maxLength, out int matchedLength)
        {
            matchedLength = 0;

            // Iterate through substrings up to maxLength to find a match
            for (int length = 1; length <= maxLength && startIndex + length <= drawingNumber.Length; length++)
            {
                string potentialMatch = drawingNumber.Substring(startIndex, length);
                if (BeltSeries.Options.ContainsKey(potentialMatch))
                {
                    matchedLength = length;
                    return potentialMatch;
                }
            }

            // If no match is found, return the longest segment and set matchedLength to maxLength
            matchedLength = maxLength;
            return drawingNumber.Substring(startIndex, maxLength);
        }

        private void LogAttributeResult(string attributeName, string codeSegment, string drawingRequestValue)
        {
            if (drawingRequestValue == "Unknown")
            {
                _logger.LogWarning("Unknown code '{CodeSegment}' for attribute '{AttributeName}'. Unable to map to a known value.", codeSegment, attributeName);
            }
            else
            {
                _logger.LogInformation("Mapped code '{CodeSegment}' to value '{DrawingRequestValue}' for attribute '{AttributeName}'.", codeSegment, drawingRequestValue, attributeName);
            }
        }
    }
}