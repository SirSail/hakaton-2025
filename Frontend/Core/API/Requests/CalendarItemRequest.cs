namespace Core.API.Requests
{
    public class CalendarItemRequest
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
