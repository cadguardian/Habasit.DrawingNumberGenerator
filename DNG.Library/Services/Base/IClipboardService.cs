using Microsoft.JSInterop;

public interface IClipboardService
{
    Task CopyTextToClipboardAsync(IJSRuntime jsRuntime, string text);
}