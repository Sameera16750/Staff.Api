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
        return performanceReview.Id;
    }

    #endregion

    #region GET Methods

    public async Task<PerformanceReview?> GetPerformanceReviewByIdAsync(long id, long organizationId, StatusDto status)
    {
        logger.LogInformation("Getting performance review by id");
        var review =
            await context.PerformanceReview.Include(r => (r.StaffMember)).Include(r => r.Reviewer)
                .FirstOrDefaultAsync(r => r.Id == id && r.Status == status.PerformanceReview &&
                                          (organizationId <= 0 ||
                                           r.StaffMember!.Designation!.Department!.OrganizationId == organizationId));
        if (review == null)
        {
            logger.LogError("performance review not found");
        }

        logger.LogInformation("performance review founded");
        return review;
    }

    public async Task<PaginatedListDto<PerformanceReview>?> GetAllPerformanceReviewsAsync(
        PerformanceReviewFilterDto filters, long organizationId, StatusDto status)
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
             (filters.DepartmentId == 0 || filters.DepartmentId == p.StaffMember!.Designation!.DepartmentId) &&
             (organizationId <= 0 || organizationId == p.StaffMember!.Designation!.Department!.OrganizationId)));
        var count = await query.CountAsync();
        var reviews = await query
            .OrderBy(d => d.Id)
            .Skip((filters.PageNumber - 1) * filters.PageSize).Take(filters.PageSize).ToListAsync();
        var response = PaginatedListDto<PerformanceReview>.Create(source: reviews, pageNumber: filters.PageNumber,
            pageSize: filters.PageSize, totalItems: count);
        return response;
    }

    #endregion

    #region PUT Methods

    public async Task<long> UpdatePerformanceReviewAsync(PerformanceReview performanceReview, long organizationId)
    {
        logger.LogInformation("checking available reviews...");
        var exist = await context.PerformanceReview.FirstOrDefaultAsync(r =>
            (r.Status == Constants.Status.Active && r.Id==performanceReview.Id &&
             r.StaffMember!.Designation!.Department!.OrganizationId == organizationId));

        if (exist == null)
        {
            logger.LogError("review not found");
            return Constants.ProcessStatus.NotFound;
        }

        logger.LogInformation("Updating review");
        context.Entry(exist).CurrentValues.SetValues(performanceReview);
        context.Entry(exist).State = EntityState.Modified;
        var result = await context.SaveChangesAsync();

        if (result < 1)
        {
            logger.LogWarning("Failed to update review");
            return Constants.ProcessStatus.Failed;
        }

        logger.LogInformation("Review updated");
        return performanceReview.Id;
    }

    #endregion
}