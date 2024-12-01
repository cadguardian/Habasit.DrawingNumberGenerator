using DNG.Library.Models;
using DNG.Library.Models.BeltSpecs;
using DNG.Library.Models.Base;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using DNG.Library.Services.Base;

namespace DNG.Library.Services;

public class DrawingRequestProcessor : IDrawingRequestProcessor
{
    [Inject] public IStateContainer State { get; set; }
    [Inject] public IPartNumberService PartNumberService { get; }

    public void GenerateDrawingNumber()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(State!.DrawingRequest!.BeltType!))
            {
                throw new ArgumentException("Belt Type is required.");
            }

            // Business logic to generate the drawing number
            State.DrawingNumber.BeltTypeCode = RuleWithOptions.GetCodeByName(State.DrawingRequest.BeltType, BeltType.Options);
            State.DrawingNumber.BeltSeriesCode = State.DrawingRequest.BeltSeries;
            State.DrawingNumber.ColorCode = RuleWithOptions.GetCodeByName(State.DrawingRequest.Color, MaterialColor.Options);
            State.DrawingNumber.MaterialCode = RuleWithOptions.GetCodeByName(State.DrawingRequest.Material, BeltMaterial.Options);
            State.DrawingNumber.AdderMaterialCode = RuleWithOptions.GetCodeByName(State.DrawingRequest.AdderMaterial, AdderMaterial.Options);
            State.DrawingNumber.RodMaterialCode = RuleWithOptions.GetCodeByName(State.DrawingRequest.RodMaterial, RodMaterial.Options);
            State.DrawingNumber.BeltWidthCode = BeltWidth.Create(State.DrawingRequest.BeltWidth).Code;
            State.DrawingNumber.FlightsRollersGripsCode = RuleWithOptions.GetCodeByName(State.DrawingRequest.FlightsRollersGrip, Flights_Rollers_Grips.Options);
            State.DrawingNumber.QtyRollersAcrossWidth = Flights_Rollers_Grips.GetFRGQuantityAcrossWidthCode(State.DrawingRequest.QtyRollersAcrossWidth);
            State.DrawingNumber.FRGCenters = Flights_Rollers_Grips.GetFRGCentersCode(State.DrawingRequest.FRGCenters);
            State.DrawingNumber.BeltAccessoriesCode = RuleWithOptions.GetCodeByName(State.DrawingRequest.BeltAccessories, BeltAccessories.Options);
            State.DrawingNumber.FrictionAntiStaticCode = RuleWithOptions.GetCodeByName(State.DrawingRequest.FrictionAntiStatic, FrictionAntiStatic.Options);
            State.DrawingNumber.SidePLLaneDVCode = RuleWithOptions.GetCodeByName(State.DrawingRequest.SidePLLaneDV, SidePLLaneDV.Options);
            State.DrawingNumber.UniqueIdentification = State.DrawingRequest.UniqueIdentification.ToUpper();
            State.DrawingNumber.IndentCode = IndentCode.GetCodeByName(State.DrawingRequest.IndentCode, IndentCode.Options);

            Console.WriteLine($"Generated Drawing Number {DateTime.Now}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Drawing # Incomplete: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        }
    }

    public string SerializeDrawingData()
    {
        var partNumbers = PartNumberService.FilterPartNumbers(State.DrawingRequest);

        var options = new JsonSerializerOptions { WriteIndented = true };
        var dataToSerialize = new
        {
            State.DrawingRequest,
            State.DrawingNumber,
            partNumbers
        };
        return JsonSerializer.Serialize(dataToSerialize, options);
    }
}