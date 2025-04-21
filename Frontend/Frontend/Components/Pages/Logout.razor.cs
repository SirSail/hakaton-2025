
using Frontend.Components.BaseClassess;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components.Pages
{
    public partial class Logout : CustomComponentBase
    {
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            SecureStorage.Remove("auth_token");

            CustomAuthStateProvider.NotifyUserLogout();
            NavigationManager.NavigateTo("/login");
        }
    }
}
