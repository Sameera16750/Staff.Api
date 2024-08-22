using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Staff.Core.Entities.Organization
{
    [Table("OrganizationDetails")]
    public class OrganizationDetails
    {
        [Key] public long Id { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string Address { get; set; }

        [Required] public string ContactNo { get; set; }

        [Required] public string Email { get; set; }

        [Required] public int Status { get; set; }

        public ICollection<Department>? Departments { get; set; } = null;
    }
}