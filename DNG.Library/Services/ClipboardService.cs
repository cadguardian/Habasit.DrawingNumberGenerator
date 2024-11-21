using DNG.Library.Services.Base;
using Microsoft.JSInterop;

namespace DNG.Library.Services;

public class ClipboardService : IClipboardService
{
    public async Task CopyTextToClipboardAsync(IJSRuntime jsRuntime, string text)
    {
        await jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
    }
}