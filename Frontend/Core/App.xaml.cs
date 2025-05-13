using Core.Authorize.Services;
using Core.PushNotifications.Services;

namespace Core
{
    public partial class App : Application
    {
        private readonly PushNotificationsService _pushNotificationService;

        public App(PushNotificationsService notificationService)
        {
            InitializeComponent();
            MainPage = new MainPage();

            _pushNotificationService = notificationService;
#if ANDROID

            Plugin.Firebase.CloudMessaging.CrossFirebaseCloudMessaging.Current.NotificationReceived += (s, message) =>
            {
                var title = message.Notification?.Title ?? "Brak tytułu";
                var body = message.Notification?.Body ?? string.Empty;

                string redirectRoute = string.Empty;
                message.Notification.Data.TryGetValue("redirect_route", out redirectRoute);

                _pushNotificationService.RaiseNotification(new PushNotification { Title = title, Body = body, RedirectRoute = redirectRoute });

                Core.Platforms.Android.Services.PushNotificationService.ShowNotification(title, body, redirectRoute);
            };

            //Plugin.Firebase.CloudMessaging.CrossFirebaseCloudMessaging.Current.TokenChanged += async (s, token) =>
            //{
            //    await _authService.RegisterFCMToken();
            //};
#endif
        }
    }
}

