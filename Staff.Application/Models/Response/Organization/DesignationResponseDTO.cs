using Staff.Core.Entities.Organization;

namespace Staff.Application.Models.Response.Organization;

public class DesignationResponseDto
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public long DepartmentId { get; set; }

    public string DepartmentName { get; set; } = string.Empty;

    public DesignationResponseDto MapToResponse(Designation designation)
    {
        return new DesignationResponseDto
        {
            Id = designation.Id,
            Name = designation.Name,
            Description = designation.Description ?? "",
            DepartmentId = designation.DepartmentId,
            DepartmentName = designation.Department!.Name
        };
    }

    public List<DesignationResponseDto> MapToListResponse(List<Designation> designations)
    {
        return designations.Select(MapToResponse).ToList();
    }
}