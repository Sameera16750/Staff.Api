using Staff.Core.Entities.Attendance;

namespace Staff.Infrastructure.Repositories.Interfaces.Attendance;

public interface IAttendanceDetailsRepo
{
    #region POST Methods

    Task<long> SaveAttendanceDetailsAsync(AttendanceDetails attendanceDetails);

    #endregion

    #region GET Methods

    Task<AttendanceDetails?> GetAttendanceDetailsAsync(DateTime attendanceDate, long staffId, int status);

    #endregion

    #region PUT Methods

    Task<long> UpdateAttendanceDetailsAsync(AttendanceDetails attendanceDetails);

    #endregion
}