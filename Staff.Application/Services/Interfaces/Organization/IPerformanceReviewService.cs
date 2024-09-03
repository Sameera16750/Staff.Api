using Staff.Application.Models.Request.common;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Infrastructure.Models.Staff;

namespace Staff.Application.Services.Interfaces.Organization;

public interface IPerformanceReviewService
{
    #region POST Methods

    Task<ResponseWithCode<dynamic>> SavePerformanceReview(PerformanceReviewRequestDto request);

    #endregion

    #region GET Methods

    Task<ResponseWithCode<dynamic>> GetPerformanceReviewByIdAsync(long id, StatusDto status);

    Task<ResponseWithCode<dynamic>> GetAllPerformanceReviewsAsync(PerformanceReviewFilterDto filters, StatusDto status);

    #endregion
}