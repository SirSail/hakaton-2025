

using Android.App;
using Android.Runtime;

namespace Core.Platforms.Android
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(nint handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp()
        {
            Firebase.FirebaseApp.InitializeApp(this);
            return MauiProgram.CreateMauiApp();
        }
    }
}
