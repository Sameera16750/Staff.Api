using Staff.Application.Models.Request.Attendance;
using Staff.Application.Models.Request.common;
using Staff.Application.Models.Response.Common;
using Staff.Infrastructure.Models.Attendance;
using Staff.Infrastructure.Models.Common;

namespace Staff.Application.Services.Interfaces.Attendance;

public interface IAttendanceDetailsService
{
    #region POST Methods

    Task<ResponseWithCode<dynamic>> SaveAttendanceAsync(SaveAttendanceRequestDto request, long organizationId);

    #endregion

    #region GET Methods

    Task<ResponseWithCode<dynamic>> GetAttendanceAsync(long id,long organizationId);
    Task<ResponseWithCode<dynamic>> GetAllAttendanceDetailsAsync(AttendanceFiltersDto filters,
        StatusDto status, long organizationId);

    #endregion

    #region PUT Methods

    Task<ResponseWithCode<dynamic>> UpdateAttendanceDetailsAsync(UpdateAttendanceRequestDto request, long id,
        long organizationId);

    #endregion

    #region DELETE Methods

    Task<ResponseWithCode<dynamic>> DeleteAttendanceDetailsAsync(long id, long organizationId);

    #endregion
}