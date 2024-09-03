using Staff.Application.Models.Request.common;
using Staff.Core.Entities.Organization;
using Staff.Infrastructure.Models;
using Staff.Infrastructure.Models.Staff;

namespace Staff.Infrastructure.Repositories.Interfaces.Organization;

public interface IPerformanceReviewRepo
{
    #region POST Methods

    Task<long> SavePerformanceReviewAsync(PerformanceReview performanceReview);

    #endregion

    #region GET Methods

    Task<PerformanceReview?> GetPerformanceReviewByIdAsync(long id, StatusDto status);

    Task<PaginatedListDto<PerformanceReview>?> GetAllPerformanceReviewsAsync(PerformanceReviewFilterDto filters,
        StatusDto status);

    #endregion
}