using Domain.Models;
using FirebaseAdmin.Messaging;
using Infrastructure.DatabaseAbstractions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Logging;

namespace Application.Notifications.Services
{
    public class PushNotificationService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<PushNotificationService> _logger;
             
        public PushNotificationService(UnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        {
            _unitOfWork = unitOfWork;
            _logger = loggerFactory.CreateLogger<PushNotificationService>();
        }

        public async Task<bool> SendPushNotificationAsync(Notification notification, SystemUser receiver,string redirectRoute,CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(receiver.CurrentFCMToken))
            {
                var message = new Message()
                {
                    Token = receiver.CurrentFCMToken,
                    Notification = notification,
                    Android = new AndroidConfig()
                    {
                        Priority = Priority.High
                    },
                };
                if (!string.IsNullOrEmpty(redirectRoute))
                {
                    message.Data = new Dictionary<string, string>() { { "redirect_route", redirectRoute } };
                }

                var result = await FirebaseMessaging.DefaultInstance.SendAsync(message, cancellationToken);

                _logger.LogInformation($"Próba przesłania powiadomienia push: {result}\n\n Token: {receiver.CurrentFCMToken}");
                return true;
            }
            else
            {
                _logger.LogInformation($"Próba przesłania powiadomienia push: użytkownik z emailem {receiver.Email} nie ma podanego FCMtoken");
                return false;
            }
        }

        public async Task<bool> SendPushNotificationAsync(Notification notification, int receiverId, string redirectRoute, CancellationToken cancellationToken)
        {
            var receiver = _unitOfWork.UserRepository.FindOrThrow(receiverId);

            return await SendPushNotificationAsync(notification, receiver, redirectRoute, cancellationToken);
        }

    }
}
