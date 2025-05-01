namespace Core.API.Services
{
    public class NotificationService
    {
        public event Action<string>? OnNotificationReceived;

        public void RaiseNotification(string message)
        {
            OnNotificationReceived?.Invoke(message);
        }
    }
}
