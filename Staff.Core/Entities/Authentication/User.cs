using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Staff.Core.Entities.Authentication;

[Table("User")]
public class User
{
    [Key]
    public long Id { get; set; }
}