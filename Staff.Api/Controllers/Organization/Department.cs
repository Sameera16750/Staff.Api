using Microsoft.AspNetCore.Mvc;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Application.Models.Response.Organization;
using Staff.Application.Services.Interfaces.Organization;

namespace Staff.Api.Controllers.Organization
{
    [Route("api/[controller]")]
    [ApiController]
    public class Department(IDepartmentService departmentService) : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpPost("save")]
        public async Task<IActionResult> SaveDepartment([FromBody] DepartmentRequestDto requestDto)
        {
            var result = await departmentService.SaveDepartment(requestDto);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepartmentResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartments(long id)
        {
            var result = await departmentService.GetDepartment(id);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(long id, [FromBody] DepartmentRequestDto requestDto)
        {
            var result = await departmentService.UpdateDepartment(requestDto, id);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }
    }
}