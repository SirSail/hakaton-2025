using Core.API;
using Core.API.Requests;
using Core.API.Responses;
using Core.API.Services;
using Core.API.StateProviders;
using Core.Components.BaseClassess;
using Microsoft.AspNetCore.Components;
using Plugin.Firebase.CloudMessaging;

namespace Core.Components.Pages
{
    public partial class Login : CustomComponentBase
    {
        private LoginRequest _loginRequest = new();
        private string _passwordFieldType = "password";

        [Inject] private ApiService ApiService { get; set; }
#if ANDROID
        [Inject] private IFirebaseCloudMessaging CloudMessaging { get; set; }
#endif
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
                string fcmtoken = string.Empty;
#if ANDROID
                fcmtoken = await CloudMessaging.GetTokenAsync();
#endif

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

                    if(!string.IsNullOrEmpty(fcmtoken))
                    {
                        await ApiService.PostAsync("api/v1/set-fcm-token", new { FCMToken = fcmtoken });
                    }
                    NavigationManager.NavigateTo("/", true);
                }
                else
                {
                    Debug = ApiService.Debug;
                }
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