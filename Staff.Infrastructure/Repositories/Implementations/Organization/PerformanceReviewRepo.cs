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
        var query = context.PerformanceReview.Where(p =>
        (
            (status.PerformanceReview == Constants.Status.All || p.Status == status.PerformanceReview) &&
            (status.Staff == Constants.Status.All || p.StaffMember!.Status == status.Staff) &&
            (status.Staff == Constants.Status.All || status.Staff == p.Reviewer!.Status) &&
            (filters.DepartmentId == 0 || (p.StaffMember!.Designation!.DepartmentId == filters.DepartmentId ||
                                           p.Reviewer!.Designation!.DepartmentId == filters.DepartmentId)) &&
            (filters.ReviewerId == 0 || filters.ReviewerId == p.ReviewerId) &&
            (filters.StaffId == 0 || filters.StaffId == p.StaffMemberId) &&
            (filters.OrganizationId == 0 ||
             filters.OrganizationId == p.StaffMember!.Designation!.Department!.OrganizationId) &&
            (p.StaffMember!.FirstName.Contains(filters.Search) || p.StaffMember.LastName!.Contains(filters.Search) ||
             p.Reviewer!.FirstName.Contains(filters.Search) || p.Reviewer.LastName!.Contains(filters.Search))));
        var count = await query.CountAsync();
        var reviews = await query
            .Include(p => p.Reviewer)
            .Include(p => p.StaffMember)
            .OrderBy(d => d.Id)
            .Skip((filters.PageNumber - 1) * filters.PageSize).Take(filters.PageSize).ToListAsync();
        var response = PaginatedListDto<PerformanceReview>.Create(source: reviews, pageNumber: filters.PageNumber,
            pageSize: filters.PageSize, totalItems: count);
        return response;
    }

    #endregion
}