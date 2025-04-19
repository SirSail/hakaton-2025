using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("patient_infos")]
    public class PatientInfo
    {
        [Key,ForeignKey("SystemUser")]
        [Column("user_id")]
        public int Id { get; set; }
        public SystemUser SystemUser { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("birth_date")]
        public DateOnly BirthDate { get; set; }
    }
}
