using System.ComponentModel.DataAnnotations;
using Staff.Core.Entities.Organization;

namespace Staff.Application.Models.Request.Organization
{
    public class DepartmentRequestDto
    {
        [Required(ErrorMessage = "Organization is required")]
        public required long Organization { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        public required string Name { get; set; }

        public Department MapToEntity(DepartmentRequestDto departmentRequestDto, int status)
        {
            return new Department()
            {
                Id = 0,
                OrganizationId = departmentRequestDto.Organization,
                Name = departmentRequestDto.Name,
                Status = status
            };
        }
    }
}