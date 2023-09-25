using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using WorkshopShared;

namespace WorkshopClient.Features.Conferences
{
    public partial class ConferenceCollection
    {
        [Inject] private HttpClient _httpClient { get; set; } = default!;
        [Inject] private NavigationManager _navigationManager { get; set; } = default!;

        private bool _isLoading = true;
        private IEnumerable<ConferenceOverview> _conferences = Enumerable.Empty<ConferenceOverview>();

        protected override async Task OnInitializedAsync()
        {
            _conferences =
                await _httpClient.GetFromJsonAsync<IEnumerable<ConferenceOverview>>("conferences")
                ?? Enumerable.Empty<ConferenceOverview>();

            _isLoading = false;
            await base.OnInitializedAsync();
        }

        private void NavigateToDetails(string mode, Guid id)
        {
            _navigationManager.NavigateTo($"/conferences/detail/{mode}/{id}");
        }
    }
}
