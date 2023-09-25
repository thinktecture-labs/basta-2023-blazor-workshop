using Microsoft.AspNetCore.Components;

namespace WorkshopClient.Features.Authentication
{
    public partial class Authentication
    {
        [Parameter]
        public string Action { get; set; } = string.Empty;
    }
}
