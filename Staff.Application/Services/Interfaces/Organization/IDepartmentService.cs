using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;

namespace Staff.Application.Services.Interfaces.Organization;

public interface IDepartmentService
{
    Task<ResponseWithCode<dynamic>> SaveDepartment(DepartmentRequestDto requestDto);
    Task<ResponseWithCode<dynamic>> GetDepartment(long id);

    Task<ResponseWithCode<dynamic>> GetAllDepartments(string search, int pageNumber, int pageSize, int departmentStatus,
        long organization, int organizationStatus);

    Task<ResponseWithCode<dynamic>> UpdateDepartment(DepartmentRequestDto requestDto, long id);
}