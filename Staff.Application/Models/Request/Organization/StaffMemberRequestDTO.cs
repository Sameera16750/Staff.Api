using System.ComponentModel.DataAnnotations;
using Staff.Core.Constants;
using Staff.Core.Entities.Organization;

namespace Staff.Application.Models.Request.Organization;

public class StaffMemberRequestDto
{
    [Required(ErrorMessage = "First Name is required")]
    public required string FirstName { get; set; }

    public string? LastName { get; set; }

    [Required(ErrorMessage = "Date of Birth is required")]
    public required DateTime Birthday { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public required string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone Number is required")]
    [Phone(ErrorMessage = "Invalid Phone Number")]
    public required string ContactNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Designation is required")]
    public long DesignationId { get; set; }

    public StaffMember MapToEntity(StaffMemberRequestDto staffMemberRequestDto)
    {
        return new StaffMember
        {
            Id = 0,
            FirstName = staffMemberRequestDto.FirstName,
            LastName = staffMemberRequestDto.LastName,
            Birthday = staffMemberRequestDto.Birthday,
            Address = staffMemberRequestDto.Address,
            ContactNumber = staffMemberRequestDto.ContactNumber,
            Email = staffMemberRequestDto.Email,
            DesignationId = staffMemberRequestDto.DesignationId,
            Status = Constants.Status.Active,
        };
    }
}