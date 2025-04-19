using Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("system_users")]
    public class SystemUser
    {
        public const int MIN_PASSWORD_LENGTH = 8;

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("role")]
        public Role Role { get; set; }  
        [Column("email")]
        public string Email { get; set; }

        [Column("password_hash")]
        [MinLength(MIN_PASSWORD_LENGTH)]
        public string PasswordHash { get; set; }

    }
}
