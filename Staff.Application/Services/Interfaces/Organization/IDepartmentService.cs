using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;

namespace Staff.Application.Services.Interfaces.Organization;

public interface IDepartmentService
{
    #region POST Methods

    Task<ResponseWithCode<dynamic>> SaveDepartmentAsync(DepartmentRequestDto requestDto, long organizationId);

    #endregion

    #region GET Methods

    Task<ResponseWithCode<dynamic>> GetDepartmentAsync(long id,long organizationId);

    Task<ResponseWithCode<dynamic>> GetAllDepartmentsAsync(string search, int pageNumber, int pageSize,
        int departmentStatus,
        long organization, int organizationStatus);

    #endregion

    #region PUT Methods

    Task<ResponseWithCode<dynamic>>
        UpdateDepartmentAsync(DepartmentRequestDto requestDto, long id, long organizationId);

    #endregion

    #region DELETE Methods

    Task<ResponseWithCode<dynamic>> DeleteDepartmentAsync(long id,long organizationId);

    #endregion
}