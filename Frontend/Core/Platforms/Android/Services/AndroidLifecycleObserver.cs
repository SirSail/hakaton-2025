#if ANDROID
using AndroidX.Lifecycle;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application = Android.App.Application;
namespace Core.Platforms.Android.Services
{
    public class AndroidLifecycleObserver : Java.Lang.Object, ILifecycleObserver
    {
        public static bool IsAppForeground { get; private set; }

        [Lifecycle.Event.OnStart]
        [Export("OnStart")]
        public void OnStart()
        {
            IsAppForeground = true;
            System.Diagnostics.Debug.WriteLine("🔥 App is in foreground");
        }

        [Lifecycle.Event.OnStop]
        [Export("OnStop")]
        public void OnStop()
        {
            IsAppForeground = false;
            System.Diagnostics.Debug.WriteLine("🌑 App is in background");
        }

        public static void Init()
        {
            ProcessLifecycleOwner.Get().Lifecycle.AddObserver(new AndroidLifecycleObserver());
        }
    }
}
#endif