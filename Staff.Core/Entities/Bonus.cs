using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Staff.Core.Entities
{
    [Table("Bonus")]
    public class Bonus
    {
        [Key] 
        public long Id { get; set; }

        [Required]
        public string Name { get; set; } 

        [Required]
        public Double Amount { get; set; } 

        [Required]
        public int Status { get; set; }
    }
}