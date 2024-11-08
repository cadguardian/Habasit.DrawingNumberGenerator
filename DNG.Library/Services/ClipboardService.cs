using Microsoft.JSInterop;

public class ClipboardService : IClipboardService
{
    public async Task CopyTextToClipboardAsync(IJSRuntime jsRuntime, string text)
    {
        await jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
    }
}