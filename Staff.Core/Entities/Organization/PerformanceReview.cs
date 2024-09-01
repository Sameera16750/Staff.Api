using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Staff.Core.Entities.Organization
{
    [Table("PerformanceReviews")]
    public class PerformanceReview
    {
        [Key] public long Id { get; set; }

        [Required] public required long StaffMemberId { get; set; }

        [Required] public required DateTime ReviewDate { get; set; }

        [Required] public required long ReviewerId { get; set; }

        [Required] public required double ReviewRating { get; set; }

        [Required] public required string ReviewComment { get; set; }

        [Required] public required int Status { get; set; }


        public StaffMember? Reviewer { get; set; } = null;

        public StaffMember? StaffMember { get; set; } = null;
    }
}