using Staff.Application.Models.Request.Attendance;
using Staff.Application.Models.Response.Common;
using Staff.Core.Entities.Attendance;
using Staff.Infrastructure.Models.Common;

namespace Staff.Application.Services.Interfaces.Attendance;

public interface ILeaveService
{
    #region POST Methods

    Task<ResponseWithCode<dynamic>> SaveLeaveTypeAsync(SaveLeaveType leaveType, long organizationId);

    #endregion

    #region GET Methods

    Task<ResponseWithCode<dynamic>> GetLeaveTypeAsync(long id, long organizationId);

    Task<ResponseWithCode<dynamic>> GetAllLeaveTypesAsync(PaginationDto filters,
        StatusDto status, long organizationId);

    #endregion
}