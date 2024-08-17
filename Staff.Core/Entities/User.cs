using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Staff.Core.Entities;

[Table("User")]
public class User
{
    [Key]
    public long Id { get; set; }
}