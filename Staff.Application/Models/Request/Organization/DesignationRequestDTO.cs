using System.ComponentModel.DataAnnotations;
using Staff.Core.Entities.Organization;

namespace Staff.Application.Models.Request.Organization
{
    public class DesignationRequestDto
    {
        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public required long DepartmentId { get; set; }

        public Designation MapToEntity(DesignationRequestDto request, int status)
        {
            return new Designation()
            {
                Name = request.Name,
                DepartmentId = request.DepartmentId,
                Status = status,
            };
        }
    }
}