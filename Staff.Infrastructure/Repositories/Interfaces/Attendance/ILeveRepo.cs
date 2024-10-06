using Staff.Core.Entities.Attendance;
using Staff.Infrastructure.Models;
using Staff.Infrastructure.Models.Common;

namespace Staff.Infrastructure.Repositories.Interfaces.Attendance;

public interface ILeveRepo
{
    #region POST Methods

    Task<long> SaveLeaveTypeAsync(LeaveType leaveType);

    #endregion

    #region GET Methods

    Task<LeaveType?> GetLeaveTypeByNameAndStatusAsync(string type, long status, long organizationId);

    Task<LeaveType?> GetLeaveTypeByIdAsync(long id, int status, long organizationId);

    Task<PaginatedListDto<LeaveType>?> GetAllLeaveTypesAsync(PaginationDto filters, StatusDto statusDto,
        long organizationId);

    #endregion
}