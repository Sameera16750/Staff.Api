using Staff.Core.Entities.Organization;

namespace Staff.Infrastructure.Repositories.Interfaces.Organization;

public interface IPerformanceReviewRepo
{
    #region POST Methods

    Task<long>SavePerformanceReviewAsync(PerformanceReview performanceReview);

    #endregion
}