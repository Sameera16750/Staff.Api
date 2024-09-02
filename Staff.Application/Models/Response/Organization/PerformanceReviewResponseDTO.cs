using Staff.Core.Entities.Organization;

namespace Staff.Application.Models.Response.Organization;

public class PerformanceReviewResponseDto
{
     public long Id { get; set; }
     public long StaffMemberId { get; set; }
     public string StaffMemberName { get; set; } = null!;
     public DateTime ReviewDate { get; set; }
     public long ReviewerId { get; set; }
     public string ReviewerName { get; set; } = null!;
     public double ReviewRating { get; set; }
     public string ReviewComment { get; set; } = null!;

     public PerformanceReviewResponseDto MapToResponse(PerformanceReview review)
     {
          return new PerformanceReviewResponseDto()
          {
               Id = review.Id,
               StaffMemberId = review.StaffMemberId,
               StaffMemberName = $"{review.StaffMember!.FirstName} {review.StaffMember!.LastName}",
               ReviewRating = review.ReviewRating,
               ReviewerId = review.ReviewerId,
               ReviewerName = $"{review.Reviewer!.FirstName} {review.Reviewer!.LastName}",
               ReviewDate = review.ReviewDate,
               ReviewComment = review.ReviewComment,
          };
     }
}