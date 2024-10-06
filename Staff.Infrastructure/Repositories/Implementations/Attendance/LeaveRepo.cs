using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Staff.Core.Constants;
using Staff.Core.Entities.Attendance;
using Staff.Infrastructure.DBContext;
using Staff.Infrastructure.Models;
using Staff.Infrastructure.Models.Common;
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

    public async Task<LeaveType?> GetLeaveTypeByIdAsync(long id, int status, long organizationId)
    {
        logger.LogInformation($"Get leave type using id: {id} and organization: {organizationId}");
        var type = await context.LeaveType.FirstOrDefaultAsync(l =>
            (l.Id == id && l.OrganizationId == organizationId && l.Status == status));
        if (type == null)
        {
            logger.LogWarning("No leave type found");
            return null;
        }

        logger.LogInformation("leave type found");
        return type;
    }

    public async Task<PaginatedListDto<LeaveType>?> GetAllLeaveTypesAsync(PaginationDto filters, StatusDto statusDto,
        long organizationId)
    {
        var query = context.LeaveType.Where(l =>
            ((organizationId <= 0 || l.OrganizationId == organizationId) && (l.Type.Contains(filters.Search)) &&
             l.Status == statusDto.LeaveType));
        var count = await query.CountAsync();
        var types = await query.OrderBy(l => l.Id)
            .Skip((filters.PageNumber - 1) * filters.PageSize).Take(filters.PageSize).ToListAsync();
        if (count >= 1)
            return PaginatedListDto<LeaveType>.Create(source: types, pageNumber: filters.PageNumber,
                pageSize: filters.PageSize, totalItems: count);
        logger.LogWarning("No leave type found");
        return null;
    }

    #endregion
}