using Core.Components.BaseClassess;
using Microsoft.AspNetCore.Components;

namespace Core.Components.Pages
{
    public partial class Logout : CustomComponentBase
    {
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            SecureStorage.Remove("auth_token");

            CustomAuthStateProvider.NotifyUserLogout();
            NavigationManager.NavigateTo("/login",true);
        }
    }
}
