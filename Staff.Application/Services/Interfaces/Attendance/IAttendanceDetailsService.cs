using Staff.Application.Models.Request.Attendance;
using Staff.Application.Models.Request.common;
using Staff.Application.Models.Response.Common;
using Staff.Infrastructure.Models.Attendance;

namespace Staff.Application.Services.Interfaces.Attendance;

public interface IAttendanceDetailsService
{
    #region POST Methods

    Task<ResponseWithCode<dynamic>> SaveAttendanceAsync(SaveAttendanceRequestDto request, long organizationId);

    #endregion

    #region GET Methods

    Task<ResponseWithCode<dynamic>> GetAllAttendanceDetailsAsync(AttendanceFiltersDto filters,
        StatusDto status,long  organizationId);

    #endregion
}