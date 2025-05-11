using Core.API.Models.Enums;

namespace Core.API.Models
{
    public class CalendarItemModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime Time { get; set; }

        public CalendarItemType Type { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }
        public DateTime? NotificationTime { get; set; }
    }
}
