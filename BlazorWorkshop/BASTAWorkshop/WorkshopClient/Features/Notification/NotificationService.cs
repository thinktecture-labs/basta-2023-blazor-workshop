using Microsoft.JSInterop;
using WorkshopClient.Utils;

namespace WorkshopClient.Features.Notification;

public class NotificationService : JSModule
{
    public NotificationService(IJSRuntime jsRuntime)
        : base(jsRuntime, "./js/nofitications.js")
    {   
    }

    public async Task ShowNotificationAsync(string title, string message)
    {
        await InvokeVoidAsync("showConferenceNotification", title, message);
    }
}
