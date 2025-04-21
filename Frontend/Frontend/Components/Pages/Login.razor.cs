using Frontend.API;
using Frontend.API.Requests;
using Frontend.API.Responses;
using Frontend.API.Services;
using Frontend.API.StateProviders;
using Frontend.Components.BaseClassess;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components.Pages
{
    public partial class Login : CustomComponentBase
    {
        private LoginRequest _loginRequest = new();
        private string _passwordFieldType = "password";

        [Inject] private ApiService ApiService { get; set; }
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
                ApiResponse<LoginResponse> apiResponse = await ApiService.PostWithResultAsync<LoginRequest, LoginResponse>($"api/v1/authorize", _loginRequest);
                if (!apiResponse.IsSuccess)
                {
                    Debug = apiResponse.Error.Message;

                    if (string.IsNullOrEmpty(Debug))
                    {
                        Debug = $"BEZ WIADOMOSCI: {ApiService.Debug}";
                    }
                    return;
                }
                LoginResponse response = apiResponse.Data;

                string token = response.Token;
                if (!string.IsNullOrEmpty(token))
                {
                    await SecureStorage.SetAsync("auth_token", token);
                    CustomAuthStateProvider.NotifyUserAuthentication(token);

                    NavigationManager.NavigateTo("/", true);
                }
                else
                {
                    Debug = ApiService.Debug;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
            }
        }

        protected void TogglePasswordVisibility()
        {
            _passwordFieldType = _passwordFieldType == "password" ? "text" : "password";
        }

        
    }
}