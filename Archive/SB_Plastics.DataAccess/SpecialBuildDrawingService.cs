using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DNG.Library.Services;
using DNG.Library.Models;
using Microsoft.Extensions.Logging;

namespace DNG.Library.Features
{
    public class SpecialBuildDrawingService
    {
        private readonly DrawingFileService _drawingFileService;
        private readonly DrawingNumberDecipherService _decipherService;
        private readonly DatabaseService _databaseService;
        private readonly ILogger<SpecialBuildDrawingService> _logger;

        public SpecialBuildDrawingService(
            DrawingFileService drawingFileService,
            DrawingNumberDecipherService decipherService,
            DatabaseService databaseService,
            ILogger<SpecialBuildDrawingService> logger)
        {
            _drawingFileService = drawingFileService;
            _decipherService = decipherService;
            _databaseService = databaseService;
            _logger = logger;
        }

        /// <summary>
        /// Scans the directory for DWG files, qualifies them, and stores valid ones in the database.
        /// </summary>
        public async Task ProcessDrawingFilesAsync(string rootFolderPath)
        {
            try
            {
                _logger.LogInformation("Scanning directory for DWG files...");

                // Step 1: Load all DWG files from directory
                var allFiles = Directory.GetFiles(rootFolderPath, "*.dwg", SearchOption.AllDirectories).ToList();
                _logger.LogInformation("Found {FileCount} DWG files.", allFiles.Count);

                var validDrawings = new List<DrawingNumber>();
                var invalidDrawings = new List<string>();

                // Step 2: Validate each file
                foreach (var filePath in allFiles)
                {
                    var filename = Path.GetFileNameWithoutExtension(filePath);
                    var drawingAttributes = _decipherService.DecipherDrawingNumber(filename);

                    if (IsInvalidDrawing(drawingAttributes))
                    {
                        _logger.LogWarning("Invalid drawing format: {Filename} (Too many unknown/invalid values)", filename);
                        invalidDrawings.Add(filePath);
                        continue;
                    }

                    var drawingNumber = new DrawingNumber
                    {
                        BeltTypeCode = drawingAttributes.ContainsKey("BeltType") ? drawingAttributes["BeltType"].DrawingCode : "",
                        BeltSeriesCode = drawingAttributes.ContainsKey("BeltSeries") ? drawingAttributes["BeltSeries"].DrawingCode : "",
                        ColorCode = drawingAttributes.ContainsKey("Color") ? drawingAttributes["Color"].DrawingCode : "",
                        MaterialCode = drawingAttributes.ContainsKey("Material") ? drawingAttributes["Material"].DrawingCode : "",
                        AdderMaterialCode = drawingAttributes.ContainsKey("AdderMaterial") ? drawingAttributes["AdderMaterial"].DrawingCode : "",
                        RodMaterialCode = drawingAttributes.ContainsKey("RodMaterial") ? drawingAttributes["RodMaterial"].DrawingCode : "",
                        BeltWidthCode = drawingAttributes.ContainsKey("BeltWidth") ? drawingAttributes["BeltWidth"].DrawingCode : "",
                        FlightsRollersGripsCode = drawingAttributes.ContainsKey("FlightsRollersGrips") ? drawingAttributes["FlightsRollersGrips"].DrawingCode : "",
                        QtyRollersAcrossWidth = drawingAttributes.ContainsKey("QtyRollersAcrossWidth") ? drawingAttributes["QtyRollersAcrossWidth"].DrawingCode : "",
                        FRGCenters = drawingAttributes.ContainsKey("FRGCenters") ? drawingAttributes["FRGCenters"].DrawingCode : "",
                        BeltAccessoriesCode = drawingAttributes.ContainsKey("BeltAccessories") ? drawingAttributes["BeltAccessories"].DrawingCode : "",
                        FrictionAntiStaticCode = drawingAttributes.ContainsKey("FrictionAntiStatic") ? drawingAttributes["FrictionAntiStatic"].DrawingCode : "",
                        SidePLLaneDVCode = drawingAttributes.ContainsKey("SidePLLaneDV") ? drawingAttributes["SidePLLaneDV"].DrawingCode : "",
                        UniqueIdentification = drawingAttributes.ContainsKey("UniqueIdentification") ? drawingAttributes["UniqueIdentification"].DrawingCode : "",
                        IndentCode = drawingAttributes.ContainsKey("IndentCode") ? drawingAttributes["IndentCode"].DrawingCode : "",
                        DrawingCode = filename
                    };

                    validDrawings.Add(drawingNumber);
                }

                _logger.LogInformation("Qualified {ValidCount} valid drawings. {InvalidCount} were invalid.", validDrawings.Count, invalidDrawings.Count);

                // Step 3: Store valid drawings in the database
                await _databaseService.InsertDrawingsAsync(validDrawings);

                _logger.LogInformation("Drawing processing completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing drawing files.");
            }
        }

        /// <summary>
        /// Checks if the drawing should be marked invalid (2 or more attributes are "Unknown" or "Invalid").
        /// </summary>
        private bool IsInvalidDrawing(Dictionary<string, (string DrawingCode, string DrawingRequestValue)> attributes)
        {
            int invalidCount = attributes.Values.Count(attr => attr.DrawingRequestValue.Equals("Unknown", StringComparison.OrdinalIgnoreCase)
                                                              || attr.DrawingRequestValue.StartsWith("Invalid", StringComparison.OrdinalIgnoreCase));
            return invalidCount >= 2;
        }

        /// <summary>
        /// Retrieves all stored drawings from the database.
        /// </summary>
        public async Task<List<DrawingNumber>> GetStoredDrawingsAsync()
        {
            return await _databaseService.GetAllDrawingsAsync();
        }
    }
}