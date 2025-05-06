namespace Core.PushNotifications.Services
{
    public class PushNotificationsService
    {
        public event Action<PushNotification>? OnNotificationReceived;

        public void RaiseNotification(PushNotification pushNotification)
        {
            OnNotificationReceived?.Invoke(pushNotification);
        }
    }
}
