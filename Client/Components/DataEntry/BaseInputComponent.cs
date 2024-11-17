using DNG.Library.Services.Base;
using Microsoft.AspNetCore.Components;

public class BaseInputComponent : ComponentBase
{
    protected override void OnInitialized()
    {
        if (State != null)
        {
            State.OnStateChange += StateHasChanged;
        }
        else
        {
            Console.WriteLine("State is null in OnInitialized!");
        }
    }

    public void Dispose()
    {
        if (State != null)
        {
            State.OnStateChange -= StateHasChanged;
        }
    }

    [Inject] protected IDrawingRequestProcessor? DrawingRequestProcessor { get; set; }
    [Inject] protected IPartNumberService? PartNumberService { get; set; }
    [Inject] protected IStateContainer? State { get; set; }

    // EventCallback to notify parent component (layout) when the value changes
    [Parameter] public EventCallback OnValueChanged { get; set; }

    // Shared input handling logic to generate drawing number and update query string
    protected async Task HandleInputAsync()
    {
        if (State == null)
        {
            throw new NullReferenceException("State is not initialized.");
        }

        if (State.DrawingRequest == null)
        {
            throw new NullReferenceException("DrawingRequest is not initialized in State.");
        }

        if (DrawingRequestProcessor == null)
        {
            throw new NullReferenceException("DrawingRequestProcessor is not initialized although injected.");
        }

        DrawingRequestProcessor!.GenerateDrawingNumber();

        UpdateQueryString();

        UpdateDrawingCode();

        // Notify parent component (layout) that the value has changed
        if (OnValueChanged.HasDelegate)
        {
            await OnValueChanged.InvokeAsync();
        }

        Console.WriteLine("Input Handled!");
    }

    private void UpdateDrawingCode()
    {
        State.UpdateProperty(State.DrawingNumber, dn => dn.DrawingCode = State.DrawingNumber!.GetDrawingNumber());
    }

    private void UpdateQueryString()
    {
        State.UpdateProperty(State.DrawingRequest, dr => dr.QueryString = State.DrawingNumber!.GetDrawingQueryString(State.DrawingNumberViewModel));
    }
}