using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;

namespace WorkshopClient.Features.Authentication
{
    public partial class LoginDisplay
    {
        [Inject] private NavigationManager _navigationManager { get; set; } = default!;
        private void BeginSignOut(MouseEventArgs args)
        {
            _navigationManager.NavigateToLogout("authentication/logout");
        }

    }
}
