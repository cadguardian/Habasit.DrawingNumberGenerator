using DNG.Library.Models;
using DNG.Library.Models.Base;
using System.Text.Json;

public class StateContainer : IStateContainer
{
    // Shared state models
    public IDrawingRequest DrawingRequest { get; private set; } = new DrawingRequest();

    public DrawingNumberViewModel DrawingNumberViewModel { get; private set; } = new DrawingNumberViewModel();

    public IDrawingNumber DrawingNumber { get; private set; } = new DrawingNumber();

    // Event for state change
    public event Action? OnStateChange;

    // Notify subscribers of state change
    private void NotifyStateChanged() => OnStateChange?.Invoke();

    // Generic property updater
    public void UpdateProperty<T>(T model, Action<T> updateAction)
    {
        updateAction(model);
        NotifyStateChanged();
    }

    // Clear all state
    public void ClearState()
    {
        DrawingRequest = new DrawingRequest();
        DrawingNumber = new DrawingNumber();
        LogChange("State cleared");
        NotifyStateChanged();
    }

    // Save state to JSON
    public string SaveStateToJson()
    {
        var state = new
        {
            DrawingRequest,
            DrawingNumber
        };
        var json = JsonSerializer.Serialize(state, new JsonSerializerOptions { WriteIndented = true });
        LogChange("State saved to JSON");
        return json;
    }

    // Load state from JSON
    public void LoadStateFromJson(string json)
    {
        var state = JsonSerializer.Deserialize<StateContainerJson>(json);
        if (state != null)
        {
            DrawingRequest = state.DrawingRequest ?? new DrawingRequest();
            DrawingNumber = state.DrawingNumber ?? new DrawingNumber();
            LogChange("State loaded from JSON");
            NotifyStateChanged();
        }
        else
        {
            LogChange("Failed to load state from JSON");
        }
    }

    // Logging with NLP insights
    private void LogChange(string message)
    {
        Console.WriteLine($"[{DateTime.UtcNow}] State Change: {message} State:{this}");
    }

    // Helper class for JSON serialization
    private class StateContainerJson
    {
        public DrawingRequest? DrawingRequest { get; set; }
        public DrawingNumber? DrawingNumber { get; set; }
    }
}