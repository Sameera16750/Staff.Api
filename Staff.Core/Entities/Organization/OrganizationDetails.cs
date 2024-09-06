using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Staff.Core.Entities.Organization
{
    [Table("OrganizationDetails")]
    public class OrganizationDetails
    {
        [Key] public long Id { get; set; }

        [Required] public required string Name { get; set; }

        [Required] public required  string Address { get; set; }

        [Required] public required  string ContactNo { get; set; }

        [Required] public required  string Email { get; set; }

        [Required] public int Status { get; set; }
        
        [Required] public required string ApiKey { get; set; }
        
        [Required] public required  DateTime ExpireDate { get; set; }

        public ICollection<Department>? Departments { get; set; } = null;
    }
}