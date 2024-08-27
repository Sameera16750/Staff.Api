using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;

namespace Staff.Application.Services.Interfaces.Organization;

public interface IDesignationService
{
    #region POST Methods

    Task<ResponseWithCode<dynamic>> SaveDesignationAsync(DesignationRequestDto request);

    #endregion

    #region GET Methods

    Task<ResponseWithCode<dynamic>> GetDesignationByIdAsync(long id);

    Task<ResponseWithCode<dynamic>> GetAllDesignationAsync(string search, int pageNumber, int pageSize,
        int designationStatus, long department, int departmentStatus, int organizationStatus);

    #endregion

    #region PUT Methods

    Task<ResponseWithCode<dynamic>> UpdateDesignationAsync(DesignationRequestDto request, long id);

    #endregion
}