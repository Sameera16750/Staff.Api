using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Staff.Core.Entities.Organization;

namespace Staff.Core.Entities.Attendance
{
    [Table("Attendance")]
    public class AttendanceDetails
    {
        [Key] 
        public long Id { get; set; }

        [Required]
        public long StaffMemberId { get; set; }

        [Required] 
        public DateTime Date { get; set; }
        
        public DateTime? CheckIn { get; set; }
        
        public DateTime? CheckOut { get; set; }

        [Required] 
        public int Status { get; set; }
        
        public StaffMember? StaffMember { get; set; }
    }
}