using Microsoft.AspNetCore.Mvc;
using Staff.Application.Models.Request.Attendance;
using Staff.Application.Models.Response.Attendance;
using Staff.Application.Models.Response.Common;
using Staff.Application.Services.Interfaces.Attendance;
using Staff.Core.Constants;
using Staff.Infrastructure.Models.Common;

namespace Staff.Api.Controllers.Attendance
{
    [Route("api/[controller]")]
    [ApiController]
    public class Leave(ILeaveService service) : ControllerBase
    {
        #region POST Methods

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpPost("Type/Save")]
        public async Task<IActionResult> SaveLeaveType([FromBody] SaveLeaveType requestDto)
        {
            var organizationId = (long)HttpContext.Items[Constants.Headers.OrganizationId]!;
            var result = await service.SaveLeaveTypeAsync(requestDto, organizationId);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion

        #region GET Methods
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LeaveTypeResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpGet("Type/{id}")]
        public async Task<IActionResult> GetLeaveType(long id)
        {
            var organizationId = (long)HttpContext.Items[Constants.Headers.OrganizationId]!;
            var result = await service.GetLeaveTypeAsync(id, organizationId);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedListResponseDto<LeaveTypeResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpGet("Type/GetAll")]
        public async Task<IActionResult> GetAllLeaveTypesAsync([FromQuery] PaginationDto filters)
        {
            var organizationId = (long)HttpContext.Items[Constants.Headers.OrganizationId]!;
            var result = await service.GetAllLeaveTypesAsync(filters, new StatusDto(), organizationId);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }
        #endregion
    }
}