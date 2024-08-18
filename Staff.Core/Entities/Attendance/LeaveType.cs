using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Staff.Core.Entities.Attendance
{
    [Table("LeaveType")]
    public class LeaveType
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int Status { get; set; }
    }
}
