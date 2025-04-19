namespace API.Requests
{
    public class GetUserCalendarItemsRequest
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
