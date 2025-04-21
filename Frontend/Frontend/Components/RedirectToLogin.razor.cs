
using Microsoft.AspNetCore.Components;

namespace Frontend.Components
{
    public partial class RedirectToLogin
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            NavigationManager.NavigateTo("/login");
        }
    }
}
