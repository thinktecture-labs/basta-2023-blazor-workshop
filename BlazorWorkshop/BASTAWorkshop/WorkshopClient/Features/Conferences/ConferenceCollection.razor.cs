using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using WorkshopClient.Services;
using WorkshopShared;

namespace WorkshopClient.Features.Conferences
{
    public partial class ConferenceCollection
    {
        [Inject] private HttpClient _httpClient { get; set; } = default!;
        [Inject] private RealTimeService _realTimeService { get; set; } = default!;
        [Inject] private NavigationManager _navigationManager { get; set; } = default!;

        private bool _isLoading = true;
        private Guid _updatedId = Guid.Empty;
        private IEnumerable<ConferenceOverview> _conferences = Enumerable.Empty<ConferenceOverview>();

        protected override async Task OnInitializedAsync()
        {
            _realTimeService.OnConferenceUpdated = async (conference) =>
            {
                var item = _conferences.FirstOrDefault(c => c.Id == conference.Id);
                if (item is not null)
                {
                    item.Title = conference.Title;
                    _updatedId = conference.Id;
                    StateHasChanged();
                }
            };

            _conferences =
                await _httpClient.GetFromJsonAsync<IEnumerable<ConferenceOverview>>("conferences")
                ?? Enumerable.Empty<ConferenceOverview>();

            _isLoading = false;
            await base.OnInitializedAsync();
        }

        private void NavigateToDetails(string mode, Guid? id = null)
        {
            _navigationManager.NavigateTo(
                id is null ? $"/conferences/detail/{mode}" : $"/conferences/detail/{mode}/{id}");
        }
    }
}
