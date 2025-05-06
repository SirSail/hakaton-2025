namespace Core.PushNotifications.Services
{
    public class PushNotification
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

        public string RedirectRoute { get; set; } = string.Empty;
    }

}
