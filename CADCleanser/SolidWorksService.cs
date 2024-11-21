using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CADCleanser
{
    public class SolidWorksService : ISolidWorksService
    {
        private SldWorks _swApp;
        private ModelDoc2 _modelDoc;

        public SolidWorksService()
        {
            ConnectToSolidWorks();
        }

        private void ConnectToSolidWorks()
        {
            try
            {
                _swApp = (SldWorks)GetActiveCOMObject("SldWorks.Application");
            }
            catch (COMException)
            {
                Type swType = Type.GetTypeFromProgID("SldWorks.Application");
                _swApp = (SldWorks)Activator.CreateInstance(swType);
                _swApp.Visible = false;
            }

            if (_swApp == null)
            {
                throw new Exception("Unable to connect to SOLIDWORKS.");
            }
        }

        [DllImport("oleaut32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        private static extern void GetActiveObject(ref Guid rclsid, IntPtr reserved, [MarshalAs(UnmanagedType.Interface)] out object ppunk);

        [DllImport("ole32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        private static extern int CLSIDFromProgID([MarshalAs(UnmanagedType.LPWStr)] string progID, out Guid clsid);

        private static object GetActiveCOMObject(string progID)
        {
            CLSIDFromProgID(progID, out Guid clsid);
            GetActiveObject(ref clsid, IntPtr.Zero, out object comObject);
            return comObject;
        }

        public void OpenFile(string filePath)
        {
            int errors = 0;
            int warnings = 0;
            int docType = GetDocumentType(filePath);

            if (docType == (int)swDocumentTypes_e.swDocNONE)
            {
                throw new ArgumentException("Unsupported file type.");
            }

            _modelDoc = _swApp.OpenDoc6(filePath, docType,
                                        (int)swOpenDocOptions_e.swOpenDocOptions_Silent,
                                        "", ref errors, ref warnings);

            if (_modelDoc == null)
            {
                throw new Exception($"Failed to open file: {filePath}");
            }
        }

        private int GetDocumentType(string filePath)
        {
            string extension = System.IO.Path.GetExtension(filePath).ToLower();
            return extension switch
            {
                ".sldprt" => (int)swDocumentTypes_e.swDocPART,
                ".sldasm" => (int)swDocumentTypes_e.swDocASSEMBLY,
                ".slddrw" => (int)swDocumentTypes_e.swDocDRAWING,
                _ => (int)swDocumentTypes_e.swDocNONE,
            };
        }

        public void CloseFile()
        {
            if (_modelDoc != null)
            {
                _swApp.CloseDoc(_modelDoc.GetTitle());
                Marshal.ReleaseComObject(_modelDoc);
                _modelDoc = null;
            }
        }

        public List<string> GetBlockDefinitions()
        {
            var blockNames = new List<string>();

            // Ensure a document is open
            if (_modelDoc == null)
            {
                throw new Exception("No document is currently open in SolidWorks.");
            }

            // Get the first feature in the document
            Feature feature = (Feature)_modelDoc.FirstFeature();
            while (feature != null)
            {
                // Check if the feature is a block definition
                if (feature.GetTypeName2() == "SketchBlockDef")
                {
                    SketchBlockDefinition blockDef = (SketchBlockDefinition)feature.GetSpecificFeature2() as SketchBlockDefinition;
                    if (blockDef != null)
                    {
                        blockNames.Add(blockDef.FileName!);
                    }
                }

                // Move to the next feature
                feature = (Feature)feature.GetNextFeature();
            }

            return blockNames;
        }

        public string GetDesignLibraryPath()
        {
            // Retrieve the Design Library path from user preferences
            string designLibraryPath = _swApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swFileLocationsDesignLibrary);

            if (string.IsNullOrEmpty(designLibraryPath))
            {
                throw new Exception("Design Library path is not set in SolidWorks.");
            }

            return designLibraryPath;
        }

        public void SaveBlockToLibrary(string blockName, string destinationFolder)
        {
            try
            {
                // Ensure a document is open
                if (_modelDoc == null)
                {
                    throw new Exception("No document is currently open in SolidWorks.");
                }

                // Get the first feature in the document
                Feature feature = (Feature)_modelDoc.FirstFeature();
                while (feature != null)
                {
                    // Check if the feature is a block definition
                    if (feature.GetTypeName2() == "SketchBlockDef")
                    {
                        SketchBlockDefinition blockDef = feature.GetSpecificFeature2() as SketchBlockDefinition;
                        if (blockDef!.FileName.Equals(blockName, StringComparison.OrdinalIgnoreCase))
                        {
                            // Construct the destination path
                            string blockFileName = $"{blockName}.sldprt";
                            string blockFilePath = Path.Combine(destinationFolder, blockFileName);

                            // Insert the block definition into a new part
                            var newPart = (PartDoc)_swApp.NewPart();
                            if (newPart == null)
                            {
                                throw new Exception("Failed to create a new part for the block.");
                            }

                            var extension = ((IModelDoc2)newPart).Extension;

                            // Insert the block into the new part
                            bool insertResult = extension.SelectByID2(blockDef.FileName, "SKETCHBLOCKDEF", 0, 0, 0, false, 0, null, 0);
                            if (!insertResult)
                            {
                                throw new Exception($"Failed to insert block '{blockName}' into the new part.");
                            }

                            string title = ((IModelDoc2)newPart).GetTitle();

                            int errors = 0;
                            bool saveResult = (bool)_swApp.ActivateDoc3(title, true, (int)swRebuildOnActivation_e.swUserDecision, ref errors);

                            if (saveResult)
                            {
                                ((IModelDoc2)newPart).SaveAs(blockFilePath);
                            }
                            else
                            {
                                throw new Exception($"Failed to save block '{blockName}' to '{blockFilePath}'.");
                            }

                            // Close the new part
                            _swApp.CloseDoc(title);
                            return;
                        }
                    }

                    // Move to the next feature
                    feature = (Feature)feature.GetNextFeature();
                }

                throw new Exception($"Block '{blockName}' not found in the document.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in SaveBlockToLibrary: {ex.Message}", ex);
            }
        }
    }
}