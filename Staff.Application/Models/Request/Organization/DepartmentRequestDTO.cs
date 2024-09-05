using System.ComponentModel.DataAnnotations;
using Staff.Core.Entities.Organization;

namespace Staff.Application.Models.Request.Organization
{
    public class DepartmentRequestDto
    {

        [Required(ErrorMessage = "Department name is required")]
        public required string Name { get; set; }

        public Department MapToEntity(DepartmentRequestDto departmentRequestDto,long organization, int status)
        {
            return new Department()
            {
                Id = 0,
                OrganizationId = organization,
                Name = departmentRequestDto.Name,
                Status = status
            };
        }
    }
}