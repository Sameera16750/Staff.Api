using Staff.Core.Entities.Attendance;

namespace Staff.Infrastructure.Repositories.Interfaces.Attendance;

public interface ILeveRepo
{
    #region POST Methods

    Task<long> SaveLeaveTypeAsync(LeaveType leaveType);

    #endregion

    #region GET Methods

    Task<LeaveType?> GetLeaveTypeByNameAndStatusAsync(string type, long status, long organizationId);

    #endregion
}