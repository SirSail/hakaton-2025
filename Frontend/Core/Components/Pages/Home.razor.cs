using Core.API.Services;
using Microsoft.AspNetCore.Components;

namespace Core.Components.Pages
{
    public partial class Home
    {
        [Inject]
        private NotificationService NotificationService { get; set; }

        private void HandleNotification(string message)
        {
            Debug = $"PUSH >>>> {message}";
            InvokeAsync(StateHasChanged);
        }
    }
}
