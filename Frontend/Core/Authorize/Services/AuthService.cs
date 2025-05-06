using Core.API.Requests;
using Core.API.Responses;
using Core.API.Services;
using Core.API.StateProviders;
using Microsoft.AspNetCore.Components.Authorization;
using Core.API;

namespace Core.Authorize.Services
{
    public class AuthService
    {

        private readonly IServiceProvider _serviceProvider;

        public AuthService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<ApiError> LoginAsync(LoginRequest loginRequest)
        {
            ApiService apiService = _serviceProvider.GetRequiredService<ApiService>();
            CustomAuthStateProvider customAuthStateProvider = _serviceProvider.GetRequiredService<AuthenticationStateProvider>() as CustomAuthStateProvider;


            var response = await apiService.PostWithResultAsync<LoginRequest, LoginResponse>("api/v1/authorize", loginRequest);

            if (!response.IsSuccess)
            {
                return response.Error;
            }


            var loginResponse = response.Data;
            var token = loginResponse.Token;
            if (!string.IsNullOrEmpty(token))
            {
                await SecureStorage.SetAsync("auth_token", token);
                customAuthStateProvider.NotifyUserAuthentication(token);

#if ANDROID
                return await RegisterFCMToken(); 
#endif
            }


            return null;
        }


        public async Task<ApiError> RegisterFCMToken()
        {
            string token = await GetFCMToken();
            if (string.IsNullOrEmpty(token))
            {
                return new ApiError
                {
                    Message = "Pusty FCM Token"
                };
            }
            ApiService apiService = _serviceProvider.GetRequiredService<ApiService>();
            return await apiService.PostAsync<string>("api/v1/fcm-token", token);
        }

        public async Task<string> GetFCMToken()
        {
#if ANDROID
            var cloudMessaging = _serviceProvider.GetRequiredService<Plugin.Firebase.CloudMessaging.IFirebaseCloudMessaging>();
            return await cloudMessaging.GetTokenAsync();   
#else
            await Task.CompletedTask; // żeby nie krzyczyło 
            return string.Empty;
#endif
        }
    }
}
