using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Staff.Core.Entities
{
    [Table("Salary")]
    public class Salary
    {
        [Key] 
        public long Id { get; set; }

        [Required] 
        public Double Amount { get; set; }

        [Required]
        public List<SalaryUpdateLog> SalaryUpdateLogs { get; set; } 

        [Required] 
        public int Status { get; set; } 
    }
}