using Core.API;
using Core.API.Requests;
using Core.API.Responses;
using Core.API.Services;
using Core.API.StateProviders;
using Core.Authorize.Services;
using Core.Components.BaseClassess;
using Microsoft.AspNetCore.Components;
using Plugin.Firebase.CloudMessaging;

namespace Core.Components.Pages
{
    public partial class Login : CustomComponentBase
    {
        [Inject]
        private AuthService AuthService { get; set; }

        private LoginRequest _loginRequest = new();
        private string _passwordFieldType = "password";

        private string Debug { get; set; } = string.Empty;


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if(IsLogged)
            {
                NavigationManager.NavigateTo("/",true);
            }
        }

        private async Task HandleLogin()
        {
            try
            {
                ApiError apiError = await AuthService.LoginAsync(_loginRequest);
                if (apiError is not null)
                {
                    Debug = apiError.Message;
                    return;
                }
                NavigationManager.NavigateTo("/", true);

            }
            catch (Exception ex)
            {
                Debug = ex.ToString();
            }
        }

        protected void TogglePasswordVisibility()
        {
            _passwordFieldType = _passwordFieldType ==  "password" ? "text" : "password";
        }

        
    }
}