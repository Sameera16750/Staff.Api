using Staff.Application.Models.Request.Attendance;
using Staff.Application.Models.Response.Common;

namespace Staff.Application.Services.Interfaces.Attendance;

public interface IAttendanceDetailsService
{
    #region POST Methods

    Task<ResponseWithCode<dynamic>> SaveAttendanceAsync(SaveAttendanceRequestDto request, long organizationId);

    #endregion
}