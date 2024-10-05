using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Staff.Core.Constants;
using Staff.Core.Entities.Attendance;
using Staff.Infrastructure.DBContext;
using Staff.Infrastructure.Repositories.Interfaces.Attendance;

namespace Staff.Infrastructure.Repositories.Implementations.Attendance;

public class LeaveRepo(ApplicationDbContext context, ILogger<ILeveRepo> logger) : ILeveRepo
{
    #region POST Methods

    public async Task<long> SaveLeaveTypeAsync(LeaveType leaveType)
    {
        logger.LogInformation("saving leave type");
        await context.LeaveType.AddAsync(leaveType);
        var result = await context.SaveChangesAsync();
        if (result < 1)
        {
            logger.LogError("Leave type saving failed");
            return Constants.ProcessStatus.Failed;
        }

        logger.LogInformation("Leave type saved to DB");
        return leaveType.Id;
    }

    #endregion

    #region GET Methods

    public async Task<LeaveType?> GetLeaveTypeByNameAndStatusAsync(string type, long status, long organizationId)
    {
        logger.LogInformation($"Get leave type using name as {type}, status: {status}");
        var ex = await context.LeaveType.FirstOrDefaultAsync(l =>
            (l.Type.Contains(type) && l.Status == status && l.OrganizationId == organizationId));
        if (ex == null)
        {
            logger.LogWarning("No leave type found");
        }

        return ex;
    }

    #endregion
}