using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Staff.Core.Entities.Attendance
{
    [Table("LeaveStatus")]
    public class LeaveStatus
    {
        [Key] 
        public long Id { get; set; } 

        [Required] 
        public string StatusName { get; set; }

        [Required]
        public int Status { get; set; }
    }
}

