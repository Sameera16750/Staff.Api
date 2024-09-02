using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Staff.Application.Models.Request.common;
using Staff.Core.Constants;
using Staff.Core.Entities.Organization;
using Staff.Infrastructure.DBContext;
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

    #endregion
}