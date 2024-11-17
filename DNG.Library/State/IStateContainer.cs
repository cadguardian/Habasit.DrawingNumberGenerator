using DNG.Library.Models;
using DNG.Library.Models.Base;

public interface IStateContainer
{
    IDrawingNumber DrawingNumber { get; }
    IDrawingRequest DrawingRequest { get; }
    DrawingNumberViewModel DrawingNumberViewModel { get; }

    event Action? OnStateChange;

    void ClearState();

    void LoadStateFromJson(string json);

    string SaveStateToJson();

    void UpdateProperty<T>(T model, Action<T> updateAction);
}