using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Staff.Core.Entities;

[Table("T_Employee")]
public class EEmployee(long id, string name, string contactNo)
{
    [Key]
    public long Id { get; set; } = id;

    [Required]
    public string Name { get; set; } = name;

    [Required]
    public string ContactNo { get; set; } = contactNo;
    
    [Required]
    public string Address { get; set; } = contactNo;
}