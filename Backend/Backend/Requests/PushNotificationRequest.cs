namespace API.Requests
{
    public class PushNotificationRequest
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public string RedirectRoute { get; set; }
    }
}
