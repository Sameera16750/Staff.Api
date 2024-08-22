using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Staff.Core.Entities.Organization
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public long Id { get; set; } 

        [Required]
        public required long OrganizationId { get; set; }

        [Required]
        public required string Name { get; set; }


        public int Status {get; set;}
        
        // navigator property
        public OrganizationDetails? OrganizationDetails { get; set; } = null;
    }
}