using Staff.Application.Models.Request.Attendance;
using Staff.Application.Models.Response.Common;

namespace Staff.Application.Services.Interfaces.Attendance;

public interface ILeaveService
{
    #region POST Methods

    Task<ResponseWithCode<dynamic>> SaveLeaveType(SaveLeaveType leaveType, long organizationId);

    #endregion
}