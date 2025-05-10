using Core.Authorize.Services;
using Core.PushNotifications.Services;

namespace Core
{
    public partial class App : Application
    {
        private readonly PushNotificationsService _pushNotificationService;
        private readonly AuthService _authService;

        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();

        }
    }
}
