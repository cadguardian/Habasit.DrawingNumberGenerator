using DNG.Library.Data;
using DNG.Library.Models;
using DNG.Library.Models.BeltSpecs;
using DNG.Library.Services.Base;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

public class DrawingRequestProcessor : IDrawingRequestProcessor
{
    public DrawingRequestProcessor(IDrawingNumber drawingNumber, IDrawingRequest drawingRequest, IPartNumberService partNumberService)
    {
        DrawingNumber = drawingNumber;
        DrawingRequest = drawingRequest;
        PartNumberService = partNumberService;
    }

    [Inject] public IDrawingNumber DrawingNumber { get; set; }
    [Inject] public IDrawingRequest DrawingRequest { get; set; }
    public IPartNumberService PartNumberService { get; }

    public void GenerateDrawingNumber()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(DrawingRequest.BeltType))
            {
                throw new ArgumentException("Belt Type is required.");
            }

            // Business logic to generate the drawing number
            DrawingNumber.BeltTypeCode = RuleWithOptions.GetCodeByName(DrawingRequest.BeltType, BeltType.Options);
            DrawingNumber.BeltSeriesCode = DrawingRequest.BeltSeries;
            DrawingNumber.ColorCode = RuleWithOptions.GetCodeByName(DrawingRequest.Color, MaterialColor.Options);
            DrawingNumber.MaterialCode = RuleWithOptions.GetCodeByName(DrawingRequest.Material, BeltMaterial.Options);
            DrawingNumber.AdderMaterialCode = RuleWithOptions.GetCodeByName(DrawingRequest.AdderMaterial, AdderMaterial.Options);
            DrawingNumber.RodMaterialCode = RuleWithOptions.GetCodeByName(DrawingRequest.RodMaterial, RodMaterial.Options);
            DrawingNumber.BeltWidthCode = BeltWidth.Create(DrawingRequest.BeltWidth).Code;
            DrawingNumber.FlightsRollersGripsCode = RuleWithOptions.GetCodeByName(DrawingRequest.FlightsRollersGrip, Flights_Rollers_Grips.Options);
            DrawingNumber.QtyRollersAcrossWidth = Flights_Rollers_Grips.GetFRGQuantityAcrossWidthCode(DrawingRequest.QtyRollersAcrossWidth);
            DrawingNumber.FRGCenters = Flights_Rollers_Grips.GetFRGCentersCode(DrawingRequest.FRGCenters);
            DrawingNumber.BeltAccessoriesCode = RuleWithOptions.GetCodeByName(DrawingRequest.BeltAccessories, BeltAccessories.Options);
            DrawingNumber.FrictionAntiStaticCode = RuleWithOptions.GetCodeByName(DrawingRequest.FrictionAntiStatic, FrictionAntiStatic.Options);
            DrawingNumber.SidePLLaneDVCode = RuleWithOptions.GetCodeByName(DrawingRequest.SidePLLaneDV, SidePLLaneDV.Options);
            DrawingNumber.UniqueIdentification = DrawingRequest.UniqueIdentification.ToUpper();
            DrawingNumber.IndentCode = IndentCode.GetCodeByName(DrawingRequest.IndentCode, IndentCode.Options);

            Console.WriteLine($"Generated Drawing Number {DateTime.Now}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating drawing number: {ex.Message}");
        }
    }

    public string SerializeDrawingData()
    {
        var partNumbers = PartNumberService.FilterPartNumbers(DrawingRequest);

        var options = new JsonSerializerOptions { WriteIndented = true };
        var dataToSerialize = new
        {
            DrawingRequest,
            DrawingNumber,
            partNumbers
        };
        return JsonSerializer.Serialize(dataToSerialize, options);
    }
}

// Wrapper class to facilitate deserialization of both DrawingRequest and DrawingNumber together
public class DrawingDataWrapper
{
    public DrawingRequest? DrawingRequest { get; set; }
    public DrawingNumber? DrawingNumber { get; set; }
}