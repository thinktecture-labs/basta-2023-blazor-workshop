using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using WorkshopShared;

namespace WorkshopClient.Features.Conferences
{
    public partial class ConferenceDetail
    {
        [Inject] private HttpClient _httpClient { get; set; } = default!;
        [Inject] private NavigationManager _navigationManager { get; set; } = default!;
        [Parameter] public Guid Id { get; set; }

        private bool _isLoading = true;
        private ConferenceDetails? _conf;

        protected override async Task OnInitializedAsync()
        {
            _conf = await _httpClient.GetFromJsonAsync<ConferenceDetails>($"conferences/{Id}");
            _isLoading = false;
            await base.OnInitializedAsync();
        }

        private async Task SaveConference()
        {
            // TODO: Save conf in API
            await _httpClient.PutAsJsonAsync($"conferences/{Id}", _conf);
            _navigationManager.NavigateTo("/conferences/overview");
        }

        private void Cancel()
        {
            _navigationManager.NavigateTo("/conferences/overview");
        }
    }
}
