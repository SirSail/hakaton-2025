using Core.API.Services;
using Core.API.StateProviders;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Core.Components.BaseClassess
{
    public abstract class CustomComponentBase : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        protected ApiService ApiService { get; set; }

        protected CustomAuthStateProvider CustomAuthStateProvider => (CustomAuthStateProvider)AuthenticationStateProvider;

        protected bool IsLogged { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            AuthenticationState authState = await CustomAuthStateProvider.GetAuthenticationStateAsync();
            if(authState is not null)
            {
                IsLogged = authState.User.Identity?.IsAuthenticated ?? false;
            }
        }
    }
}
