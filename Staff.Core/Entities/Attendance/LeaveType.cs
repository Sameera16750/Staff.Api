using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Staff.Core.Entities.Organization;

namespace Staff.Core.Entities.Attendance
{
    [Table("LeaveType")]
    public class LeaveType
    {
        [Key] public long Id { get; set; }

        [Required] public required string Type { get; set; }

        [Required] public required long OrganizationId { get; set; }

        [Required] public int Status { get; set; }
        
        public OrganizationDetails? OrganizationDetails { get; set; }
    }
}