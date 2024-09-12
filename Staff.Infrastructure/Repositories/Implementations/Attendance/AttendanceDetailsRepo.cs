using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Staff.Application.Models.Request.common;
using Staff.Core.Entities.Attendance;
using Staff.Infrastructure.DBContext;
using Staff.Infrastructure.Models;
using Staff.Infrastructure.Models.Attendance;
using Staff.Infrastructure.Repositories.Interfaces.Attendance;

namespace Staff.Infrastructure.Repositories.Implementations.Attendance;

public class AttendanceDetailsRepo(ApplicationDbContext context, ILogger<IAttendanceDetailsRepo> logger)
    : IAttendanceDetailsRepo
{
    #region POST Methods

    public async Task<long> SaveAttendanceDetailsAsync(AttendanceDetails attendanceDetails)
    {
        logger.LogInformation("saving attendance details");
        context.Attendances.Add(attendanceDetails);
        var count = await context.SaveChangesAsync();
        if (count < 1)
        {
            logger.LogError("saving attendance details failed");
        }

        logger.LogInformation("saving attendance details completed");
        return attendanceDetails.Id;
    }

    #endregion

    #region GET Methods

    public async Task<AttendanceDetails?> GetAttendanceDetailsAsync(DateTime attendanceDate, long staffId, int status)
    {
        logger.LogInformation($"Getting attendance details for staff id {staffId}");
        var attendance = await context.Attendances.FirstOrDefaultAsync(a =>
            a.Date.Date == attendanceDate.Date.Date && a.StaffMemberId == staffId && a.Status == status);
        if (attendance == null)
        {
            logger.LogError($"Attendance details not found for staff id {staffId}");
        }

        return attendance;
    }

    public async Task<PaginatedListDto<AttendanceDetails>?> GetAllAttendanceDetailsAsync(AttendanceFiltersDto filters,
        StatusDto status, long organizationId)
    {
        var query = context.Attendances.Where(a =>
            (a.Status == status.Attendance &&
             (a.Date.Date >= filters.FromDate.Date) &&
             (a.Date.Date <= filters.ToDate) &&
             (filters.DepartmentId <= 0 || a.StaffMember!.Designation!.DepartmentId == filters.DepartmentId) &&
             (filters.StaffId <= 0 || a.StaffMemberId == filters.StaffId) &&
             (organizationId <= 0 || a.StaffMember!.Designation!.Department!.OrganizationId == organizationId) &&
             (a.StaffMember!.FirstName.Contains(filters.Search) || a.StaffMember!.LastName!.Contains(filters.Search))
            ));
        var count = await query.CountAsync();
        var attendance = await query.Include(a => a.StaffMember).OrderBy(a => a.Id)
            .Skip((filters.PageNumber - 1) * filters.PageSize).Take(filters.PageSize).ToListAsync();
        if (count >= 1)
            return PaginatedListDto<AttendanceDetails>.Create(source: attendance, pageNumber: filters.PageNumber,
                pageSize: filters.PageSize, totalItems: count);
        logger.LogWarning("No attendance found");
        return null;
    }

    #endregion

    #region PUT Methods

    public async Task<long> UpdateAttendanceDetailsAsync(AttendanceDetails attendanceDetails)
    {
        logger.LogInformation("updating attendance details");
        context.Attendances.Update(attendanceDetails);
        var count = await context.SaveChangesAsync();
        if (count < 1)
        {
            logger.LogError("saving attendance details failed");
        }

        logger.LogInformation("saving attendance details completed");
        return attendanceDetails.Id;
    }

    #endregion
}