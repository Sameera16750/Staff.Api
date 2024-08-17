using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Staff.Core.Entities
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