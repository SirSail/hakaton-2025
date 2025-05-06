using Core.PushNotifications.Services;
using Microsoft.AspNetCore.Components;

namespace Core.Components.Pages
{
    public partial class Home
    {
        [Inject]
        private PushNotificationsService NotificationService { get; set; }

        private void HandleNotification(string message)
        {
            Debug = $"PUSH >>>> {message}";
            InvokeAsync(StateHasChanged);
        }
    }
}
