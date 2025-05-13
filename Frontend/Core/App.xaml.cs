using Core.Authorize.Services;
using Core.PushNotifications.Services;

namespace Core
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();

        }
    }
}
