using Staff.Core.Entities.Organization;

namespace Staff.Application.Models.Response.Organization;

public class DepartmentResponseDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long OrganizationId { get; set; }
    public string OrganizationName { get; set; }

    public DepartmentResponseDto MapToResponse(Department department)
    {
        return new DepartmentResponseDto
        {
            Id = department.Id,
            Name = department.Name,
            OrganizationId = department.OrganizationId,
            OrganizationName = department.OrganizationDetails!.Name
        };
    }
}