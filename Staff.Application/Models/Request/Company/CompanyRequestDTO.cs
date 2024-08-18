using System.ComponentModel.DataAnnotations;
using Staff.Core.Entities.Company;

namespace Staff.Application.Models.Request.Company
{
    public class CompanyRequestDto
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

        public CompanyDetails MapToEntity(CompanyRequestDto companyRequestDto, int status)
        {
            return new CompanyDetails
            {
                Id = companyRequestDto.Id ?? 0,
                Address = companyRequestDto.Address,
                ContactNo = companyRequestDto.ContactNo,
                Email = companyRequestDto.Email,
                Name = companyRequestDto.Name,
                Status = status
            };
        }
    }
}