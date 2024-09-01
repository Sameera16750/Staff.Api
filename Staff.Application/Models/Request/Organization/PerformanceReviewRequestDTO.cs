using System.ComponentModel.DataAnnotations;
using Staff.Core.Constants;
using Staff.Core.Entities.Organization;

namespace Staff.Application.Models.Request.Organization;

public class PerformanceReviewRequestDto
{
    [Required(ErrorMessage = "Staff member is required")]
    public required long StaffMemberId { get; set; }

    [Required(ErrorMessage = "Review date is required")]
    public required DateTime ReviewDate { get; set; }

    [Required(ErrorMessage = "Reviewer is required")]
    public required long ReviewerId { get; set; }

    [Required(ErrorMessage = "Review rating is required")]
    public required double ReviewRating { get; set; }

    [Required(ErrorMessage = "Review comment is required")]
    public required string ReviewComment { get; set; }

    public PerformanceReview MapToEntity(PerformanceReviewRequestDto requestDto)
    {
        return new PerformanceReview
        {
            Id = 0,
            StaffMemberId = requestDto.StaffMemberId,
            ReviewerId = requestDto.ReviewerId,
            ReviewDate = requestDto.ReviewDate,
            ReviewComment = requestDto.ReviewComment,
            ReviewRating = requestDto.ReviewRating,
            Status = Constants.Status.Active
        };
    }
}