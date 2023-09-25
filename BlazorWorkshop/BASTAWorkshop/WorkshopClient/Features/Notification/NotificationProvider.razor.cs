using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WorkshopClient.Services;

namespace WorkshopClient.Features.Notification
{
    public partial class NotificationProvider
    {
        [Inject] private RealTimeService _realTimeService { get; set; } = default!;
        [Inject] private IJSRuntime _jsRuntime { get; set; } = default!;

        private IJSObjectReference? jsModule;

        protected override async Task OnInitializedAsync()
        {
            await _realTimeService.InitializeAsync();
            _realTimeService.OnConferenceAdded = async (conference) =>
            {
                Console.WriteLine(jsModule is null);
                await jsModule!.InvokeVoidAsync("showConferenceNotification", "Conference Added", $"Conference {conference.Title} has been added");
            };
            _realTimeService.OnConferenceUpdated = async (conference) =>
            {
                Console.WriteLine(jsModule is null);
                await jsModule!.InvokeVoidAsync("showConferenceNotification", "Conference Updated", $"Conference {conference.Title} has been updated");
            };
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                jsModule = await _jsRuntime.InvokeAsync<IJSObjectReference>(
                    "import", "./Features/Notification/NotificationProvider.razor.js");

                await jsModule.InvokeAsync<bool>("allowNotification");
            }
        }
    }
}
