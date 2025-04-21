using Frontend.API.StateProviders;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Frontend.Components.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private CustomAuthStateProvider CustomAuthStateProvider => (CustomAuthStateProvider)AuthenticationStateProvider;

        private bool IsLogged { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            ClaimsPrincipal userPrincipial = (await CustomAuthStateProvider.GetAuthenticationStateAsync()).User;
            IsLogged = userPrincipial.Identity?.IsAuthenticated ?? false;
        }


    }
}
