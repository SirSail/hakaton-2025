using Core.API.Services;
using Core.API.StateProviders;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Plugin.Firebase.CloudMessaging;

namespace Core
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();


            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });


            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("http://192.168.43.9:6999/") // Replace with your backend
            });

#if ANDROID
            builder.Services.AddSingleton(_ => CrossFirebaseCloudMessaging.Current);
#endif

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<ApiService>();


            builder.Services.AddAuthorizationCore();
            builder.Services.AddSingleton<AuthenticationStateProvider, CustomAuthStateProvider>();
            builder.Services.AddSingleton<NotificationService>();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
