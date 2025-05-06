using API.Attributes;
using API.Requests;
using Application.Notifications.Services;
using Domain.Models;
using FirebaseAdmin.Messaging;
using Infrastructure.DatabaseAbstractions;
using Microsoft.AspNetCore.Mvc;
using System.Management;

namespace API.Controllers
{
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly PushNotificationService _pushNotificationService;

        public NotificationsController(UnitOfWork unitOfWork, PushNotificationService pushNotificationService)
        {
            _unitOfWork = unitOfWork;
            _pushNotificationService = pushNotificationService;
        }

        [HttpPost("api/v1/push-notification")]
        public async Task<IActionResult> PushNotification([FromBody] PushNotificationRequest request,CancellationToken cancellationToken)
        {
            Notification notification = new Notification() { Title = request.Title,Body = request.Body };

            var user = _unitOfWork.UserRepository.FindOrThrow(request.UserId);
            await _pushNotificationService.SendPushNotificationAsync(notification, user,request.RedirectRoute,cancellationToken);

            return Ok();
        }

        [Authorize]
        [HttpPost("api/v1/fcm-token")]
        public IActionResult SetUserFCMToken([FromBody] string FCMToken)
        {
            SystemUser loggedUser = HttpContext.Items["User"] as SystemUser;
            loggedUser.CurrentFCMToken = FCMToken;

            _unitOfWork.UserRepository.Update(loggedUser);
            _unitOfWork.Commit();

            return Ok();
        }

    }
}
