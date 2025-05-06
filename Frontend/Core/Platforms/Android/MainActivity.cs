using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace Core.Platforms.Android
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density,LaunchMode = LaunchMode.SingleTop)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            if (intent?.Extras != null && intent.Extras.ContainsKey("route"))
            {
                var route = intent.GetStringExtra("route");
                AndroidAppState.LastRouteFromPush = route;
            }
        }

        //protected override void OnCreate(Bundle? savedInstanceState)
        //{
        //    base.OnCreate(savedInstanceState);

        //    if (Intent?.Extras != null && Intent.Extras.ContainsKey("route"))
        //    {
        //        var route = Intent.GetStringExtra("route");
        //        AndroidAppState.LastRouteFromPush = route;
        //    }
        //}
    }
}
