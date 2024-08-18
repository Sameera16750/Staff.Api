using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Staff.Core.Entities.Company;

namespace Staff.Core.Entities.Attendance
{
    [Table("Leave")]
    public class Leave
    {
        [Key] 
        public long Id { get; set; }

        [Required] 
        public StaffMember StaffMember { get; set; }

        [Required] 
        public LeaveType LeaveType { get; set; }

        [Required] 
        public DateTime StartDate { get; set; }

        [Required] 
        public DateTime EndDate { get; set; }

        [Required] 
        public LeaveStatus LeaveStatus { get; set; }

        [Required] 
        public StaffMember ApprovedBy { get; set; } 

        [Required] 
        public  int Status { get; set; }
    }
}