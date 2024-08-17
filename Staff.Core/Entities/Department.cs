using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Staff.Core.Entities
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public long Id { get; set; } 

        [Required]
        public Company Company { get; set; }

        [Required]
        public string Name { get; set; }

        public int Status {get; set;}
    }
}