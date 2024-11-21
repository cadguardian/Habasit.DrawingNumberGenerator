using Microsoft.JSInterop;

namespace DNG.Library.Services.Base;

public interface IClipboardService
{
    Task CopyTextToClipboardAsync(IJSRuntime jsRuntime, string text);
}