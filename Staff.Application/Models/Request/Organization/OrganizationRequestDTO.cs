using System.ComponentModel.DataAnnotations;
using Staff.Core.Entities.Organization;

namespace Staff.Application.Models.Request.Organization
{
    public class OrganizationRequestDto
    {
        public long? Id { get; set; } = 0;

        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public required string Address { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public required string ContactNo { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public required string Email { get; set; }

        public OrganizationDetails MapToEntity(OrganizationRequestDto organizationRequestDto, int status)
        {
            return new OrganizationDetails
            {
                Id = organizationRequestDto.Id ?? 0,
                Address = organizationRequestDto.Address,
                ContactNo = organizationRequestDto.ContactNo,
                Email = organizationRequestDto.Email,
                Name = organizationRequestDto.Name,
                Status = status
            };
        }
    }
}