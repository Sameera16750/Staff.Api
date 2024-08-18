using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Staff.Core.Entities.Company;

namespace Staff.Core.Entities.Payroll
{
    [Table("Payroll")]
    public class Payroll
    {
        [Key] 
        public long Id { get; set; }

        [Required] 
        public StaffMember StaffMember { get; set; }

        [Required]
        public Salary Salary { get; set; }
    
        [Required]
        public List<Bonus> Bonuses { get; set; }

        [Required]
        public List<Deductions> Deductions { get; set; }

        [Required]
        public  List<Tax> Taxes { get; set; } 

        [Required]
        public int Status { get; set; } 
    }
}
