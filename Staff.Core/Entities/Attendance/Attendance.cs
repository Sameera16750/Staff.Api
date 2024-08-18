using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Staff.Core.Entities.Organization;

namespace Staff.Core.Entities.Attendance
{
    [Table("Attendance")]
    public class Attendance
    {
        [Key] 
        public long Id { get; set; }

        [Required]
        public StaffMember StaffMember { get; set; }

        [Required] 
        public DateTime Date { get; set; }

        [Required] 
        public DateTime CheckIn { get; set; }

        [Required] 
        public DateTime CheckOut { get; set; }

        [Required] 
        public int Status { get; set; }
    }
}