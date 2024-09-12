using Staff.Application.Models.Request.common;
using Staff.Core.Entities.Attendance;
using Staff.Infrastructure.Models;
using Staff.Infrastructure.Models.Attendance;

namespace Staff.Infrastructure.Repositories.Interfaces.Attendance;

public interface IAttendanceDetailsRepo
{
    #region POST Methods

    Task<long> SaveAttendanceDetailsAsync(AttendanceDetails attendanceDetails);

    #endregion

    #region GET Methods

    Task<AttendanceDetails?> GetAttendanceDetailsAsync(DateTime attendanceDate, long staffId, int status);

    Task<PaginatedListDto<AttendanceDetails>?> GetAllAttendanceDetailsAsync(AttendanceFiltersDto filters,
        StatusDto status,long  organizationId);

    #endregion

    #region PUT Methods

    Task<long> UpdateAttendanceDetailsAsync(AttendanceDetails attendanceDetails);

    #endregion
}