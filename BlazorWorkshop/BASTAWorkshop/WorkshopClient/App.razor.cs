using Microsoft.AspNetCore.Components;
using WorkshopClient.Services;

namespace WorkshopClient
{
    public partial class App
    {

        [Inject] private RealTimeService _realTimeService { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await _realTimeService.InitializeAsync();
            await base.OnInitializedAsync();
        }
    }
}
