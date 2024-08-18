using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Staff.Core.Entities.Authentication;

namespace Staff.Core.Entities.Organization;

[Table("StaffMember")]
public class StaffMember {
    [Key] 
    public long Id { get; set; }

    [Required] 
    public string FirstName { get; set; }

    public string LastName { get; set; } 

    [Required] 
    public DateTime Birthday { get; set; }

    [Required] 
    public string Address { get; set; }

    [Required] 
    public string ContactNumber { get; set; }

    [Required] 
    public string Email { get; set; } 

    [Required] 
    public Department Department { get; set; }

    [Required] 
    public Role Role { get; set; } 

    [Required] 
    public OrganizationDetails OrganizationDetails { get; set; }
}