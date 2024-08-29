using Staff.Core.Entities.Organization;

namespace Staff.Application.Models.Response.Organization;

public class StaffMemberResponseDto
{
    public long Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime Birthday { get; set; }

    public string Address { get; set; } = string.Empty;

    public string ContactNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public long DesignationId { get; set; }

    public string DesignationName { get; set; } = string.Empty;

    public StaffMemberResponseDto MapToResponse(StaffMember staffMember)
    {
        return new StaffMemberResponseDto
        {
            Id = staffMember.Id,
            FirstName = staffMember.FirstName,
            LastName = staffMember.LastName ?? "",
            Birthday = staffMember.Birthday,
            Address = staffMember.Address,
            ContactNumber = staffMember.ContactNumber,
            Email = staffMember.Email ?? "",
            DesignationId = staffMember.DesignationId,
            DesignationName = staffMember.Designation!.Name,
        };
    }
}