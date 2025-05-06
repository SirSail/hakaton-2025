using Core.API.Services;
using Core.API.StateProviders;
using Core.Authorize.Services;
using Core.PushNotifications.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Plugin.Firebase.CloudMessaging;

namespace Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAndroidServices(this IServiceCollection services)
        {
#if ANDROID
            services.AddSingleton(_ => CrossFirebaseCloudMessaging.Current);
#endif

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ApiService>();
            services.AddSingleton<AuthenticationStateProvider, CustomAuthStateProvider>();
            services.AddSingleton<PushNotificationsService>();

            services.AddTransient<AuthService>();

            return services;
        }
    }
}
