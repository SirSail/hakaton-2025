
#if ANDROID
using Core.Platforms.Android;
#endif
using Microsoft.AspNetCore.Components;

namespace Core.Components
{
    public partial class Main
    {
        [Inject]
        private NavigationManager NavManager { get; set; }
#if ANDROID
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender && !string.IsNullOrEmpty(AndroidAppState.LastRouteFromPush))
            {
                var route = AndroidAppState.LastRouteFromPush;
                AndroidAppState.LastRouteFromPush = null;
                NavManager.NavigateTo(route);
            }
        }
#endif
    }
}
