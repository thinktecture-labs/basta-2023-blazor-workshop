using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using WorkshopShared;

namespace WorkshopClient.Features.Conferences
{
    public static class ConferenceMode
    {
        public static string Create = "Create";
        public static string Edit = "Edit";
        public static string Show = "Show";
    }

    public partial class ConferenceDetail
    {
        [Inject] private HttpClient _httpClient { get; set; } = default!;
        [Inject] private NavigationManager _navigationManager { get; set; } = default!;
        [Parameter] public Guid Id { get; set; }
        [Parameter] public string Mode { get; set; } = string.Empty;

        private EditForm? _editForm;
        private bool _isLoading = true;
        private bool _readonly => Mode == ConferenceMode.Show;
        private ConferenceDetails? _conf;

        protected override async Task OnInitializedAsync()
        {
            if (Mode == ConferenceMode.Create)
            {
                _conf = new ConferenceDetails();
            }
            else
            {
                _conf = await _httpClient.GetFromJsonAsync<ConferenceDetails>($"conferences/{Id}");
            }
            _isLoading = false;
            await base.OnInitializedAsync();
        }

        private async Task SaveConference()
        {
            if (_editForm?.EditContext is not null && _editForm.EditContext.Validate())
            {
                try
                {
                    var result = await _httpClient.PutAsJsonAsync($"conferences/{Id}", _conf);
                    if (result.IsSuccessStatusCode)
                    {
                        _navigationManager.NavigateTo("/conferences/overview");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async void ToggleEditMode()
        {
            if (!_readonly)
            {
                await SaveConference();
                Mode = ConferenceMode.Show;
            }
            else
            {
                Mode = ConferenceMode.Edit;
            }
        }

        private void Cancel()
        {
            _navigationManager.NavigateTo("/conferences/overview");
        }
    }
}
