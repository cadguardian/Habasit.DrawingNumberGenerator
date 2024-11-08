using Microsoft.JSInterop;

public interface INotificationService
{
    Task ShowToastAsync(IJSRuntime jsRuntime, string message);
}