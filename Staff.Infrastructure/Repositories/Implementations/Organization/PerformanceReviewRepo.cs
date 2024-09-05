using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Staff.Application.Models.Request.common;
using Staff.Core.Constants;
using Staff.Core.Entities.Organization;
using Staff.Infrastructure.DBContext;
using Staff.Infrastructure.Models;
using Staff.Infrastructure.Models.Staff;
using Staff.Infrastructure.Repositories.Interfaces.Organization;

namespace Staff.Infrastructure.Repositories.Implementations.Organization;

public class PerformanceReviewRepo(ILogger<IPerformanceReviewRepo> logger, ApplicationDbContext context)
    : IPerformanceReviewRepo
{
    #region POST Methods

    public async Task<long> SavePerformanceReviewAsync(PerformanceReview performanceReview)
    {
        logger.LogInformation("Saving performance review");
        context.PerformanceReview.Add(performanceReview);
        var result = await context.SaveChangesAsync();
        if (result < 1)
        {
            logger.LogError("Failed to save performance review");
            return Constants.ProcessStatus.Failed;
        }

        logger.LogInformation("Performance review saved");
        return result;
    }

    #endregion

    #region GET Methods

    public async Task<PerformanceReview?> GetPerformanceReviewByIdAsync(long id, StatusDto status)
    {
        logger.LogInformation("Getting performance review by id");
        var review =
            await context.PerformanceReview.Include(r => (r.StaffMember)).Include(r => r.Reviewer)
                .FirstOrDefaultAsync(r => r.Id == id && r.Status == status.PerformanceReview);
        if (review == null)
        {
            logger.LogError("performance review not found");
        }

        logger.LogInformation("performance review founded");
        return review;
    }

    public async Task<PaginatedListDto<PerformanceReview>?> GetAllPerformanceReviewsAsync(
        PerformanceReviewFilterDto filters,
        StatusDto status)
    {
        var query = context.PerformanceReview.Include(p => p.Reviewer).Include(p => p.StaffMember).Where(p =>
            (p.Status == status.PerformanceReview && (p.ReviewComment.ToLower().Contains(filters.Search.ToLower()) ||
                                                      p.Reviewer!.FirstName.ToLower()
                                                          .Contains(filters.Search.ToLower()) ||
                                                      p.Reviewer.LastName!.ToLower()
                                                          .Contains(filters.Search.ToLower()) ||
                                                      p.StaffMember!.FirstName.ToLower()
                                                          .Contains(filters.Search.ToLower()) ||
                                                      p.StaffMember.LastName!.ToLower()
                                                          .Contains(filters.Search.ToLower())) &&
             (filters.DepartmentId == 0 || filters.DepartmentId == p.StaffMember!.Designation!.DepartmentId)));
        var count = await query.CountAsync();
        var reviews = await query
            .OrderBy(d => d.Id)
            .Skip((filters.PageNumber - 1) * filters.PageSize).Take(filters.PageSize).ToListAsync();
        var response = PaginatedListDto<PerformanceReview>.Create(source: reviews, pageNumber: filters.PageNumber,
            pageSize: filters.PageSize, totalItems: count);
        return response;
    }

    #endregion
}