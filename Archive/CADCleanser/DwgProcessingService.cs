using System;
using System.Collections.Immutable;
using CADCleanser.Models;
using Aspose.CAD;
using Aspose.CAD.FileFormats.Cad;

namespace CADCleanser.Services
{
    public class DwgProcessingService : IDwgProcessingService
    {
        public ImmutableList<string> ExtractBlocks(string filePath)
        {
            try
            {
                // Load the DWG file
                using (CadImage cadImage = (CadImage)Image.Load(filePath))
                {
                    var blocks = cadImage.BlockEntities;
                    var blockNames = ImmutableList.CreateBuilder<string>();

                    foreach (var block in blocks.Keys)
                    {
                        blockNames.Add(block.ToString()!);
                    }

                    return blockNames.ToImmutable();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to process DWG file: {filePath}", ex);
            }
        }
    }
}