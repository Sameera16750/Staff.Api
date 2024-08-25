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
    public class Designation(IDesignationService designationService) : ControllerBase
    {
        #region POST Methods

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpPost("Save")]
        public async Task<IActionResult> SaveDesignationAsync([FromBody] DesignationRequestDto request)
        {
            var result = await designationService.SaveDesignationAsync(request);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion

        #region GET Methods

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DesignationResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDesignationByIdAsync(long id)
        {
            var result = await designationService.GetDesignationByIdAsync(id);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedListResponseDto<DesignationResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpGet("List")]
        public async Task<IActionResult> GetAllDesignationsAsync([FromQuery] PaginatedListRequestDto query,
            [FromQuery] long departmentId)
        {
            var result = await designationService.GetAllDesignationAsync(
                search: query.SearchTerm,
                pageNumber: query.PageNumber,
                pageSize: query.PageSize,
                department: departmentId,
                designationStatus: Constants.Status.Active,
                departmentStatus: Constants.Status.Active,
                organizationStatus: Constants.Status.Active
            );

            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion
    }
}