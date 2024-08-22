using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;

namespace Staff.Application.Services.Interfaces.Organization;

public interface IDepartmentService
{
    Task<ResponseWithCode<dynamic>> SaveDepartment(DepartmentRequestDto requestDto);
}