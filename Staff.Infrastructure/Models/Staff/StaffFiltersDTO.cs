using Staff.Infrastructure.Models.Common;

namespace Staff.Infrastructure.Models.Staff;

public class StaffFiltersDto : PaginationDto
{
    public long DesignationId { get; set; } = 0;
    public long DepartmentId { get; set; } = 0;
}