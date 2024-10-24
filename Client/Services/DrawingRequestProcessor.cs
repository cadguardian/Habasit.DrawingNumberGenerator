using DNG.Library.Data;
using DNG.Library.Models;

using DNG.Library.Models;

namespace Client.Services
{
    public class DrawingRequestProcessor : IDrawingRequestProcessor
    {
        public DrawingRequestProcessor(IDrawingNumber drawingNumber, IDrawingRequest drawingRequest)
        {
            DrawingNumber = drawingNumber;
            DrawingRequest = drawingRequest;
        }

        public IDrawingNumber DrawingNumber { get; }
        public IDrawingRequest DrawingRequest { get; }

        private bool isProcessing = false;

        public void GenerateDrawingNumber()
        {
            try
            {
                isProcessing = true;

                // Validations before processing
                if (string.IsNullOrWhiteSpace(DrawingRequest.BeltType))
                {
                    throw new ArgumentException("Belt Type is required.");
                }

                DrawingNumber.BeltTypeCode = RuleWithOptions.GetCodeByName(DrawingRequest.BeltType, BeltType.Options);
                DrawingNumber.BeltSeriesCode = DrawingRequest.BeltSeries;
                DrawingNumber.ColorCode = RuleWithOptions.GetCodeByName(DrawingRequest.Color, MaterialColor.Options);
                DrawingNumber.MaterialCode = RuleWithOptions.GetCodeByName(DrawingRequest.Material, BeltMaterial.Options);
                DrawingNumber.AdderMaterialCode = RuleWithOptions.GetCodeByName(DrawingRequest.AdderMaterial, AdderMaterial.Options);
                DrawingNumber.RodMaterialCode = RuleWithOptions.GetCodeByName(DrawingRequest.RodMaterial, RodMaterial.Options);
                DrawingNumber.BeltWidthCode = BeltWidth.Create(DrawingRequest.BeltWidth).Code;
                DrawingNumber.FlightsRollersGripsCode = RuleWithOptions.GetCodeByName(DrawingRequest.FlightsRollersGrip, Flights_Rollers_Grips.Options);
                DrawingNumber.QtyRollersAcrossWidth = "*";
                DrawingNumber.FRGCenters = "*";
                DrawingNumber.BeltAccessoriesCode = RuleWithOptions.GetCodeByName(DrawingRequest.BeltAccessories, BeltAccessories.Options);
                DrawingNumber.FrictionAntiStaticCode = RuleWithOptions.GetCodeByName(DrawingRequest.FrictionAntiStatic, FrictionAntiStatic.Options);
                DrawingNumber.SidePLLaneDVCode = RuleWithOptions.GetCodeByName(DrawingRequest.SidePLLaneDV, SidePLLaneDV.Options);
                DrawingNumber.UniqueIdentification = DrawingRequest.UniqueIdentification.ToUpper();
                DrawingNumber.IndentCode = IndentCode.GetCodeByName(DrawingRequest.IndentCode, IndentCode.Options);

                Console.WriteLine($"Generated Drawing Number {DateTime.Now}");
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, notify user, etc.)
                Console.WriteLine($"Error generating drawing number: {ex.Message}");
                // You could also implement a user-friendly error display mechanism here
            }
            finally
            {
                isProcessing = false;
            }
        }
    }
}