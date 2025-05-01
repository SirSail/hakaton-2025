using Application.Calendar.Services;
using Application.Notifications.Services;
using Domain.Models;
using FirebaseAdmin.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application.Notifications.HostedServices
{
    public class PushNotificationHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public PushNotificationHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromMinutes(1));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    using var scope = _serviceProvider.CreateScope();

                    var calendarService = scope.ServiceProvider.GetService<CalendarService>();
                    var pushNotificationService = scope.ServiceProvider.GetService<PushNotificationService>();

                    IEnumerable<CalendarItem> currentCalendarItems = await calendarService.GetCalendarItemWithNotificationTimeNow();

                    foreach (CalendarItem calendarItem in currentCalendarItems)
                    {
                        string title = "Nowe powiadomienie!";
                        string body = $"Zbliża się termin wydarzenia {calendarItem.Title} (godzina {calendarItem.Time.ToShortTimeString()}).";
                        if (!string.IsNullOrEmpty(calendarItem.Description))
                        {
                            body += $"Szczegóły: {calendarItem.Description}";
                        }

                        var notification = new Notification()
                        {
                            Title = title,
                            Body = body
                        };
                        await pushNotificationService.SendPushNotificationAsync(notification, calendarItem.UserId, $"/calendar-item/{calendarItem.Id}", stoppingToken);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Pushnotifikacje zostały zatrzymane.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd push notyfikacji >>>: {ex.ToString()}");
            }
        }

    }
}
