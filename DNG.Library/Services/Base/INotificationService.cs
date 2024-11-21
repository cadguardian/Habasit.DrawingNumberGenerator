using Microsoft.JSInterop;

namespace DNG.Library.Services.Base;

public interface INotificationService
{
    Task ShowToastAsync(IJSRuntime jsRuntime, string message);
}