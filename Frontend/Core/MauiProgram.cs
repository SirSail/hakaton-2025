using Microsoft.Extensions.Logging;

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

            
            //builder.Services.AddScoped(sp => new HttpClient
            //{
            //    BaseAddress = new Uri("http://192.168.43.9:6999/")
            //});



            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("http://158.75.95.16:6999/") 
            });


            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddCustomServices();
            builder.Services.AddAndroidServices();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
