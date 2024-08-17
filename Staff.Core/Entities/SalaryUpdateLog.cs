using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Staff.Core.Entities
{
    [Table("SalaryUpdateLog")]
    public class SalaryUpdateLog{
        [Key] 
        public long Id { get; set; }

        [Required] 
        public Double LastSalary { get; set; }

        [Required] 
        public Double CurrentSalary { get; set; }

        [Required] 
        public Double ChangedValue { get; set; } 

        [Required] 
        public string Description { get; set; }

        [Required]
        public bool isIncrement { get; set; }
    }
}