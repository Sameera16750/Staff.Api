using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Staff.Core.Entities.Organization
{
    [Table("PerformanceReviews")]
    public class PerformanceReview{
        [Key]
        public long Id { get; set; }

        [Required]
        public StaffMember StaffMember { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; } 

        [Required]
        public StaffMember Reviewer { get; set; } 

        [Required]
        public Double ReviewRating { get; set; } 

        [Required]
        public string ReviewComment { get; set; }

        [Required]
        public int Status {get; set;} 
    }
}