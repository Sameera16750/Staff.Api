using Staff.Infrastructure.Models.Common;

namespace Staff.Infrastructure.Models.Attendance;

public class AttendanceFiltersDto : PaginationDto
{
    public long StaffId { get; set; } = 0;
    public long DepartmentId { get; set; } = 0;
    public DateTime FromDate { get; set; } = new DateTime(1900, 1, 1);
    public DateTime ToDate { get; set; } = new DateTime(3000, 1, 1);
}