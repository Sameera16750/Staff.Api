using Staff.Application.Models.Request.common;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Infrastructure.Models.Common;
using Staff.Infrastructure.Models.Staff;

namespace Staff.Application.Services.Interfaces.Organization;

public interface IPerformanceReviewService
{
    #region POST Methods

    Task<ResponseWithCode<dynamic>> SavePerformanceReview(PerformanceReviewRequestDto request, long organizationId);

    #endregion

    #region GET Methods

    Task<ResponseWithCode<dynamic>> GetPerformanceReviewByIdAsync(long id, long organizationId, StatusDto status);

    Task<ResponseWithCode<dynamic>> GetAllPerformanceReviewsAsync(PerformanceReviewFilterDto filters,
        long organizationId, StatusDto status);

    #endregion
    
    #region PUT Methods

    Task<ResponseWithCode<dynamic>> UpdatePerformanceReviewAsync(PerformanceReviewRequestDto request,long id,long organizationId);

    #endregion
    
    #region DELETE Methods
    
    Task<ResponseWithCode<dynamic>> DeletePerformanceReviewAsync(long id, long organizationId);

    #endregion
}