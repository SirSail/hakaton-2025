using Core.API.Services;
using System.Diagnostics;

namespace Core
{
    public partial class App : Application
    {
        private readonly NotificationService _notificationService;


        public App(NotificationService notificationService)
        {
            InitializeComponent();
            MainPage = new MainPage();

            _notificationService = notificationService;
#if ANDROID

            Plugin.Firebase.CloudMessaging.CrossFirebaseCloudMessaging.Current.NotificationReceived += (s, message) =>
            {
                var body = message.Notification?.Body ?? "No body";
                var title = message.Notification?.Title ?? "No title";
                Debug.WriteLine($"Push received: {body}");
                _notificationService.RaiseNotification($"Push: {body}");

                Core.Platforms.Android.Services.PushNotificationService.ShowNotification(title, body);
            };

            Plugin.Firebase.CloudMessaging.CrossFirebaseCloudMessaging.Current.TokenChanged += (s, token) =>
            {
                Debug.WriteLine($"FCM Token: {token}");
            };
#endif

        }
    }
}
