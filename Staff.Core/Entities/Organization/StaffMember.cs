using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Staff.Core.Entities.Authentication;

namespace Staff.Core.Entities.Organization;

[Table("StaffMember")]
public class StaffMember {
    [Key] 
    public long Id { get; set; }

    [Required] 
    public required string FirstName { get; set; }

    public string? LastName { get; set; } 

    [Required] 
    public required DateTime Birthday { get; set; }

    [Required] 
    public required string Address { get; set; } =string.Empty;

    [Required] 
    public required string ContactNumber { get; set; } =string.Empty;

    [Required] 
    public string? Email { get; set; } 

    [Required]
    public long DesignationId { get; set; }
    
    public Designation? Designation { get; set; } 
    
    [Required] public required int Status { get; set; }
    
}