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

        [HttpGet("api/v1/calendar-items")]
        [Authorize]
        public async Task<IResult> GetUserCalendarItems([FromBody] GetUserCalendarItemsRequest request)
        {
            var calendarItems = await _calendarService.GetUserItemsFromDateSpan(LoggedUser.Id, request.StartDate, request.EndDate);
            return Results.Ok(calendarItems);

        }
    }
}
