using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Staff.Core.Entities.Organization
{
    [Table("Designation")]
    public class Designation
    {
        [Key] public long Id { get; set; }

        [Required] public required string Name { get; set; }

        public string? Description { get; set; }

        [Required] public required long DepartmentId { get; set; }

        public Department? Department { get; set; }

        [Required] public required int Status { get; set; }
        
        public ICollection<StaffMember>? StaffMembers { get; set; }
    }
}