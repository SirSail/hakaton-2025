using Domain.Models;
using Domain.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Requests
{
    public class CalendarItemRequest
    {
            [ForeignKey("User")]
            [Column("user_id")]
            public int UserId { get; set; }

            [Column("time")]
            public DateTime Time { get; set; }

            [Column("type")]
            public CalendarItemType Type { get; set; }

            [Column("title")]
            public string Title { get; set; }

            [Column("description")]
            public string? Description { get; set; }

            [Column("notification_time")]
            public DateTime? NotificationTime { get; set; }


        public CalendarItem ToCalendarItem()
        {
            return new()
            {
                UserId = UserId,
                Time = Time,
                Type = Type,
                Title = Title,
                Description = Description,
                NotificationTime = NotificationTime
            };
        }
    }
}
