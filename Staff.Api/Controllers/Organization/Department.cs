using Microsoft.AspNetCore.Mvc;
using Staff.Application.Models.Request.common;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Application.Models.Response.Organization;
using Staff.Application.Services.Interfaces.Organization;
using Staff.Core.Constants;

namespace Staff.Api.Controllers.Organization
{
    [Route("api/[controller]")]
    [ApiController]
    public class Department(IDepartmentService departmentService) : ControllerBase
    {
        #region POST Methods

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpPost("Save")]
        public async Task<IActionResult> SaveDepartmentAsync([FromBody] DepartmentRequestDto requestDto)
        {
            var result = await departmentService.SaveDepartmentAsync(requestDto);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion

        #region GET Methods

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepartmentResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpGet("Get/{id:long}")]
        public async Task<IActionResult> GetDepartmentsAsync(long id)
        {
            var result = await departmentService.GetDepartmentAsync(id);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedListResponseDto<DepartmentResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllDepartmentsAsync([FromQuery] PaginatedListRequestDto query,
            [FromQuery] long organisationId)
        {
            var result = await departmentService.GetAllDepartmentsAsync(
                search: query.SearchTerm,
                pageNumber: query.PageNumber,
                pageSize: query.PageSize,
                organization: organisationId,
                departmentStatus: Constants.Status.Active,
                organizationStatus: Constants.Status.Active
            );
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion

        #region PUT Methods

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpPut("Update/{id:long}")]
        public async Task<IActionResult> UpdateDepartmentAsync(long id, [FromBody] DepartmentRequestDto requestDto)
        {
            var result = await departmentService.UpdateDepartmentAsync(requestDto, id);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion

        #region DELETE Methods

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpDelete("Delete/{id:long}")]
        public async Task<IActionResult> DeleteDepartmentAsync(long id)
        {
            var results = await departmentService.DeleteDepartmentAsync(id);
            return new ObjectResult(results.Response) { StatusCode = (int)results.StatusCode };
        }

        #endregion
    }
}