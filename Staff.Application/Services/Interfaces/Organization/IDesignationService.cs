using Staff.Application.Models.Request.common;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Infrastructure.Models.Staff;

namespace Staff.Application.Services.Interfaces.Organization;

public interface IDesignationService
{
    #region POST Methods

    Task<ResponseWithCode<dynamic>> SaveDesignationAsync(DesignationRequestDto request, long organizationId);

    #endregion

    #region GET Methods

    Task<ResponseWithCode<dynamic>> GetDesignationByIdAsync(long id, long organizationId);

    Task<ResponseWithCode<dynamic>> GetAllDesignationAsync(DesignationFiltersDto filters, StatusDto status,long organizationId);

    #endregion

    #region PUT Methods

    Task<ResponseWithCode<dynamic>> UpdateDesignationAsync(DesignationRequestDto request, long id, long organizationId);

    #endregion

    #region DELETE Methods

    Task<ResponseWithCode<dynamic>> DeleteDesignationAsync(long id,long organizationId);

    #endregion
}