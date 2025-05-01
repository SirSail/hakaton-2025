using Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("system_users")]
    public class SystemUser
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("role")]
        public Role Role { get; set; }  
        [Column("email")]
        public string Email { get; set; }

        [Column("password_hash")]
        public string PasswordHash { get; set; }

        [Column("current_fcm_token")]
        public string CurrentFCMToken { get; set; } = string.Empty;
    }
}
