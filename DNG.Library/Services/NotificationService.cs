using Microsoft.JSInterop;

public class NotificationService : INotificationService
{
    public async Task ShowToastAsync(IJSRuntime jsRuntime, string message)
    {
        await jsRuntime.InvokeVoidAsync("eval", $@"
            document.getElementById('toastBody').innerText = '{message}';
            var toastElement = document.getElementById('copyToast');
            var toast = new bootstrap.Toast(toastElement);
            toast.show();
        ");
    }
}