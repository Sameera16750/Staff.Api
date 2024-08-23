using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;

namespace Staff.Application.Services.Interfaces.Organization;

public interface IDepartmentService
{
    #region POST Methods

    Task<ResponseWithCode<dynamic>> SaveDepartment(DepartmentRequestDto requestDto);

    #endregion

    #region GET Methods

    Task<ResponseWithCode<dynamic>> GetDepartment(long id);

    Task<ResponseWithCode<dynamic>> GetAllDepartments(string search, int pageNumber, int pageSize, int departmentStatus,
        long organization, int organizationStatus);

    #endregion

    #region PUT Methods

    Task<ResponseWithCode<dynamic>> UpdateDepartment(DepartmentRequestDto requestDto, long id);

    #endregion

    #region DELETE Methods

    Task<ResponseWithCode<dynamic>> DeleteDepartment(long id);

    #endregion
}