using API.Attributes;
using API.Requests;
using Application.Calendar.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private SystemUser LoggedUser => HttpContext.Items["User"] as SystemUser;


        private readonly CalendarService _calendarService;

        public CalendarController(CalendarService calendarService)
        {
            _calendarService = calendarService;
        }


        [HttpPost("api/v1/calendar-item")]
        public async Task<IActionResult> PostCalendarItem([FromBody] CalendarItemRequest request)
        {
            CalendarItem item = request.ToCalendarItem();
            await _calendarService.InsertCalendarItem(item);

            return Ok();
        }

        [HttpPost("api/v1/calendar-items")]
        [Authorize]
        public async Task<IResult> GetUserCalendarItems([FromBody] GetUserCalendarItemsRequest request)
        {
            var calendarItems = await _calendarService.GetUserItemsFromDateSpan(LoggedUser.Id, request.StartDate, request.EndDate);
            return Results.Ok(calendarItems);
        }



        [HttpPost("api/v1/calendar-item/{id}/remove-notification")]
        [Authorize]
        public async Task<IResult> RemoveNotification([FromRoute] int id)
        {
            var calendarItem = await _calendarService.GetCalendarItemById(id);
            if (calendarItem.UserId != LoggedUser.Id)
            {
                return Results.Forbid();
            }
            await _calendarService.RemoveCalendarItemNotification(calendarItem);
            return Results.Ok();
        }

        [HttpPost("api/v1/calendar-item/{id}/set-notification")]
        [Authorize]
        public async Task<IResult> SetNotification([FromRoute] int id, [FromBody] DateTime notificationTime)
        {
            var calendarItem = await _calendarService.GetCalendarItemById(id);
            if (calendarItem.UserId != LoggedUser.Id)
            {
                return Results.Forbid();
            }
            await _calendarService.SetCalendarItemNotification(calendarItem, notificationTime);
            return Results.Ok();
        }
    }
}
