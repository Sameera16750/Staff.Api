using Microsoft.Extensions.Logging;
using Staff.Core.Constants;
using Staff.Core.Entities.Organization;
using Staff.Infrastructure.DBContext;
using Staff.Infrastructure.Repositories.Interfaces.Organization;

namespace Staff.Infrastructure.Repositories.Implementations.Organization;

public class PerformanceReviewRepo(ILogger<IPerformanceReviewRepo> logger, ApplicationDbContext context)
    : IPerformanceReviewRepo
{
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
}