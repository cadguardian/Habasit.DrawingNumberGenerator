using DNG.Library.Data;
using DNG.Library.Models;
using DrawingNumberGenerator.Library.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Pages;

public partial class DrawingNumberGenerator
{
    private bool isProcessing = false;
    private ElementReference beltTypeDropdownRef;
    private ElementReference beltSeriesDropdownRef;
    private ElementReference colorDropdownRef;
    private ElementReference materialDropdownRef;
    private ElementReference adderMaterialDropdownRef;
    private ElementReference rodMaterialDropdownRef;
    private ElementReference beltWidthRef;
    private ElementReference flightsRollersGripDropdownRef;
    private ElementReference beltAccessoriesDropdownRef;
    private ElementReference frictionAntiStaticDropdownRef;
    private ElementReference sidePLLaneDVDropdownRef;
    private ElementReference indentCodeDropdownRef;
    private ElementReference qtyRollerWidth;
    private ElementReference frgCenters;

    private readonly List<string> beltTypes = BeltType.Options?.Keys?.Where(x => x != "").OrderBy(x => x).ToList()!;
    private readonly List<string> beltSeries = BeltSeries.Options?.Keys?.Where(x => x != "").OrderBy(x => x).ToList()!;
    private readonly List<string> colors = MaterialColor.Options?.Values?.Where(x => x != "").OrderBy(x => x).ToList()!;
    private readonly List<string> materials = BeltMaterial.Options?.Values?.Where(x => x != "").OrderBy(x => x).ToList()!;
    private readonly List<string> adderMaterials = AdderMaterial.Options?.Values?.Where(x => x != "").OrderBy(x => x).ToList()!;
    private readonly List<string> rodMaterials = RodMaterial.Options?.Values?.Where(x => x != "").OrderBy(x => x).ToList()!;
    private readonly List<string> flightsRollersGrips = Flights_Rollers_Grips.Options?.Values?.Where(x => x != "").OrderBy(x => x).ToList()!;
    private readonly List<string> beltAccessories = BeltAccessories.Options?.Values?.Where(x => x != "").OrderBy(x => x).ToList()!;
    private readonly List<string> frictionAntiStatics = FrictionAntiStatic.Options?.Values?.Where(x => x != "").OrderBy(x => x).ToList()!;
    private readonly List<string> sidePLLaneDVs = SidePLLaneDV.Options?.Values?.Where(x => x != "").OrderBy(x => x).ToList()!;
    private readonly List<string> indents = IndentCode.Options?.Values?.Where(x => x != "").ToList()!;

    [Parameter]
    public DrawingNumber DrawingNumber { get; set; } = DrawingNumber.Create();

    private readonly DrawingRequest drawingRequest = new();

    protected override void OnInitialized()
    {
        // Set the default value here
        drawingRequest.BeltType = "M";
    }

    private void GenerateDrawingNumber()
    {
        isProcessing = true;

        DrawingNumber.BeltTypeCode = drawingRequest.BeltType;
        DrawingNumber.BeltSeriesCode = drawingRequest.BeltSeries;
        DrawingNumber.ColorCode = RuleWithOptions.GetCodeByName(drawingRequest.Color, MaterialColor.Options);
        DrawingNumber.MaterialCode = RuleWithOptions.GetCodeByName(drawingRequest.Material, BeltMaterial.Options);
        DrawingNumber.AdderMaterialCode = RuleWithOptions.GetCodeByName(drawingRequest.AdderMaterial, AdderMaterial.Options);
        DrawingNumber.RodMaterialCode = RuleWithOptions.GetCodeByName(drawingRequest.RodMaterial, RodMaterial.Options);
        DrawingNumber.BeltWidthCode = BeltWidth.Create(drawingRequest.BeltWidth).Code;
        DrawingNumber.FlightsRollersGripsCode = RuleWithOptions.GetCodeByName(drawingRequest.FlightsRollersGrip, Flights_Rollers_Grips.Options);
        DrawingNumber.QtyRollersAcrossWidth = "*";
        DrawingNumber.FRGCenters = "*";
        DrawingNumber.BeltAccessoriesCode = RuleWithOptions.GetCodeByName(drawingRequest.BeltAccessories, BeltAccessories.Options);
        DrawingNumber.FrictionAntiStaticCode = RuleWithOptions.GetCodeByName(drawingRequest.FrictionAntiStatic, FrictionAntiStatic.Options);
        DrawingNumber.SidePLLaneDVCode = RuleWithOptions.GetCodeByName(drawingRequest.SidePLLaneDV, SidePLLaneDV.Options);
        DrawingNumber.UniqueIdentification = drawingRequest.UniqueIdentification.ToUpper();
        DrawingNumber.IndentCode = IndentCode.GetCodeByName(drawingRequest.IndentCode, IndentCode.Options);
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
        GenerateDrawingNumber();
        drawingRequest.QueryString = DrawingNumber.GetDrawingQueryString();

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
        var drawingNumber = DrawingNumber.GetDrawingQueryString();  // Assuming this is the method that generates the drawing number.
        await JSRuntime.InvokeVoidAsync("CopyDrawingNumber", drawingNumber);
    }
}