using DNG.Library.Data;
using DNG.Library.Models;
using DNG.Library.Services.Base;
using Microsoft.AspNetCore.Components;

public class BaseInputComponent : ComponentBase
{
    [Inject] protected IDrawingNumber? DrawingNumber { get; set; }
    [Inject] protected IDrawingRequest? DrawingRequest { get; set; }
    [Inject] protected IDrawingRequestProcessor? DrawingRequestProcessor { get; set; }
    [Inject] protected IPartNumberService? PartNumberService { get; set; }

    // EventCallback to notify parent component (layout) when the value changes
    [Parameter] public EventCallback OnValueChanged { get; set; }

    // Shared input handling logic to generate drawing number and update query string
    protected async Task HandleInputAsync()
    {
        DrawingRequestProcessor!.GenerateDrawingNumber();
        DrawingRequest!.QueryString = DrawingNumber!.GetDrawingQueryString();
        DrawingNumber!.DrawingCode = DrawingNumber!.GetDrawingNumber();

        // Notify parent component (layout) that the value has changed
        if (OnValueChanged.HasDelegate)
        {
            await OnValueChanged.InvokeAsync();
        }

        Console.WriteLine("Input Handled!");
    }
}