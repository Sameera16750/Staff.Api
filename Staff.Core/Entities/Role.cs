using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Staff.Core.Entities;

[Table("Roles")]
public class Role
{
    [Key]
    public long Id { get; set; }
}