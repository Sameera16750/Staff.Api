using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Staff.Core.Entities
{
    [Table("Tax")]
    public class Tax
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public Double Rate { get; set; }

        [Required]
        public int Status { get; set; }
    }
}