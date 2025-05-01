using Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    [Table("calendar_items")]  
    public class CalendarItem
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }

        [JsonIgnore]
        public SystemUser User { get; set; }

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
    }
}
