using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Staff.Core.Entities;

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
    public Company Company { get; set; }
}