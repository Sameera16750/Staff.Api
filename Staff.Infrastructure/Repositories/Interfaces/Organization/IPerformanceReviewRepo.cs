using Staff.Core.Entities.Organization;
using Staff.Infrastructure.Models;
using Staff.Infrastructure.Models.Common;
using Staff.Infrastructure.Models.Staff;

namespace Staff.Infrastructure.Repositories.Interfaces.Organization;

public interface IPerformanceReviewRepo
{
    #region POST Methods

    Task<long> SavePerformanceReviewAsync(PerformanceReview performanceReview);

    #endregion

    #region GET Methods

    Task<PerformanceReview?> GetPerformanceReviewByIdAsync(long id, long organizationId, StatusDto status);

    Task<PaginatedListDto<PerformanceReview>?> GetAllPerformanceReviewsAsync(PerformanceReviewFilterDto filters,
        long organizationId, StatusDto status);

    #endregion
    
    #region PUT Methods

    Task<long> UpdatePerformanceReviewAsync(PerformanceReview performanceReview,long organizationId);

    #endregion

    #region DELETE Methods
    
    Task<long> DeletePerformanceReviewAsync(long id, long organizationId);

    #endregion
}