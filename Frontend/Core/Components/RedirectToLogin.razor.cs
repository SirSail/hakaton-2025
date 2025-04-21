using Core.Components.BaseClassess;
using Microsoft.AspNetCore.Components;
namespace Core.Components
{
    public partial class RedirectToLogin : CustomComponentBase
    {
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            NavigationManager.NavigateTo("/login");
        }
    }
}
