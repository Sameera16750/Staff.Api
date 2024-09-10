using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Staff.Core.Entities.Attendance;
using Staff.Infrastructure.DBContext;
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