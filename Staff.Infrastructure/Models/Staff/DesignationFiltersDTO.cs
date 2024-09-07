using Staff.Infrastructure.Models.Common;

namespace Staff.Infrastructure.Models.Staff;

public class DesignationFiltersDto:PaginationDto
{
    public long DepartmentId { get; set; } = 0;
}