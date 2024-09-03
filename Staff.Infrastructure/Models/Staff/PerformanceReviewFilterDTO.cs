using Staff.Infrastructure.Models.Common;

namespace Staff.Infrastructure.Models.Staff;

public class PerformanceReviewFilterDto : PaginationDto
{
    public long StaffId { get; set; } = 0;
    public long ReviewerId { get; set; } = 0;
    public long DepartmentId { get; set; } = 0;
}