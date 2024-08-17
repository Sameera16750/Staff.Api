using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Staff.Core.Entities
{
    [Table("Company")]
    public class Company
    {
        [Key] public long Id { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string Address { get; set; }

        [Required] public string ContactNo { get; set; } 

        [Required] public string Email { get; set; } 

        [Required]
        public int Status { get; set; }
    }    
}

