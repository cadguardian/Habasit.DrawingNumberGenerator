using DNG.Library.Data;
using DNG.Library.Models;
using DrawingNumberGenerator.Library.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Pages;

public partial class DrawingNumberGenerator
{
    private readonly DrawingNumberInput userInput = new();
    private bool isProcessing = false;
    private ElementReference beltTypeDropdownRef;
    private ElementReference beltSeriesDropdownRef;
    private ElementReference colorDropdownRef;
    private ElementReference materialDropdownRef;
    private ElementReference adderMaterialDropdownRef;
    private ElementReference rodMaterialDropdownRef;
    private ElementReference flightsRollersGripDropdownRef;
    private ElementReference beltAccessoriesDropdownRef;
    private ElementReference frictionAntiStaticDropdownRef;
    private ElementReference sidePLLaneDVDropdownRef;

    private readonly List<string> beltTypes = BeltType.Options?.Values?.Where(x => x != "").OrderBy(x => x).ToList()!;
    private readonly List<string> beltSeries = BeltSeries.Options?.Keys?.Where(x => x != "").OrderBy(x => x).ToList()!;
    private readonly List<string> colors = MaterialColor.Options?.Values?.Where(x => x != "").OrderBy(x => x).ToList()!;
    private readonly List<string> materials = BeltMaterial.Options?.Values?.Where(x => x != "").OrderBy(x => x).ToList()!;
    private readonly List<string> adderMaterials = AdderMaterial.Options?.Values?.Where(x => x != "").OrderBy(x => x).ToList()!;
    private readonly List<string> rodMaterials = RodMaterial.Options?.Values?.Where(x => x != "").OrderBy(x => x).ToList()!;
    private readonly List<string> flightsRollersGrips = Flights_Rollers_Grips.Options?.Values?.Where(x => x != "").OrderBy(x => x).ToList()!;
    private readonly List<string> beltAccessories = BeltAccessories.Options?.Values?.Where(x => x != "").OrderBy(x => x).ToList()!;
    private readonly List<string> frictionAntiStatics = FrictionAntiStatic.Options?.Values?.Where(x => x != "").OrderBy(x => x).ToList()!;
    private readonly List<string> sidePLLaneDVs = SidePLLaneDV.Options?.Values?.Where(x => x != "").OrderBy(x => x).ToList()!;

    private DrawingNumber DrawingNumber { get; set; } = DrawingNumber.Create();

    private void GenerateDrawingNumber()
    {
        isProcessing = true;

        DrawingNumber.BeltTypeCode = RuleWithOptions.GetCodeByName(userInput.BeltType, BeltType.Options);
        DrawingNumber.BeltSeriesCode = userInput.BeltSeries;
        DrawingNumber.ColorCode = RuleWithOptions.GetCodeByName(userInput.Color, MaterialColor.Options);
        DrawingNumber.MaterialCode = RuleWithOptions.GetCodeByName(userInput.Material, BeltMaterial.Options);
        DrawingNumber.AdderMaterialCode = RuleWithOptions.GetCodeByName(userInput.AdderMaterial, AdderMaterial.Options);
        DrawingNumber.RodMaterialCode = RuleWithOptions.GetCodeByName(userInput.RodMaterial, RodMaterial.Options);
        DrawingNumber.BeltWidthCode = BeltWidth.Create(userInput.BeltWidth).Code;
        DrawingNumber.FlightsRollersGripsCode = RuleWithOptions.GetCodeByName(userInput.FlightsRollersGrip, Flights_Rollers_Grips.Options);
        DrawingNumber.QtyRollersAcrossWidth =
        //DrawingNumber.FRGCenters
        DrawingNumber.BeltAccessoriesCode = RuleWithOptions.GetCodeByName(userInput.BeltAccessories, BeltAccessories.Options);
        DrawingNumber.FrictionAntiStaticCode = RuleWithOptions.GetCodeByName(userInput.FrictionAntiStatic, FrictionAntiStatic.Options);
        DrawingNumber.SidePLLaneDVCode = RuleWithOptions.GetCodeByName(userInput.SidePLLaneDV, SidePLLaneDV.Options);
        DrawingNumber.UniqueIdentification = userInput.UniqueIdentification.ToUpper();
        DrawingNumber.IndentCode = userInput.IndentCode.ToString("0.##");
        isProcessing = false;
    }

    private async Task HandleKeyDown(KeyboardEventArgs e, ElementReference dropdownRef)
    {
        if (e.Key == "Enter" || e.Key == " " || e.Key == "ArrowDown")
        {
            await OpenDropdown(dropdownRef);
        }
    }

    private Task HandleBlur(ElementReference dropdownRef)
    {
        return CloseDropdown(dropdownRef);
    }

    private async Task OpenDropdown(ElementReference dropdownRef)
    {
        await JSRuntime.InvokeVoidAsync("openDropdown", dropdownRef);
    }

    private async Task CloseDropdown(ElementReference dropdownRef)
    {
        await JSRuntime.InvokeVoidAsync("closeDropdown", dropdownRef);
    }

    private async Task CopyDrawingNumberToClipboard()
    {
        var drawingNumber = DrawingNumber.GetDrawingNumber();  // Assuming this is the method that generates the drawing number.
        await JSRuntime.InvokeVoidAsync("CopyDrawingNumber", drawingNumber);
    }
}