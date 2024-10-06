using Microsoft.AspNetCore.Mvc;
using Staff.Application.Models.Request.Attendance;
using Staff.Application.Models.Request.common;
using Staff.Application.Models.Response.Attendance;
using Staff.Application.Models.Response.Common;
using Staff.Application.Services.Interfaces.Attendance;
using Staff.Core.Constants;
using Staff.Infrastructure.Models.Attendance;
using Staff.Infrastructure.Models.Common;

namespace Staff.Api.Controllers.Attendance
{
    [Route("api/[controller]")]
    [ApiController]
    public class Attendance(IAttendanceDetailsService service) : ControllerBase
    {
        #region POST Methods

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpPost("CheckInCheckOut")]
        public async Task<IActionResult> CheckingCheckout([FromBody] SaveAttendanceRequestDto requestDto)
        {
            var organizationId = (long)HttpContext.Items[Constants.Headers.OrganizationId]!;
            var result = await service.SaveAttendanceAsync(requestDto, organizationId);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion

        #region GET Methods

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AttendanceResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpGet("Get/{id:long}")]
        public async Task<IActionResult> GetAttendanceDetailsByIdAsync(long id)
        {
            var organizationId = (long)HttpContext.Items[Constants.Headers.OrganizationId]!;
            var result = await service.GetAttendanceAsync(id, organizationId);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedListResponseDto<AttendanceResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAttendanceAsync([FromQuery] AttendanceFiltersDto filters)
        {
            var organizationId = (long)HttpContext.Items[Constants.Headers.OrganizationId]!;
            var result = await service.GetAllAttendanceDetailsAsync(filters, new StatusDto(), organizationId);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion

        #region PUT Methods

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpPut("Update/{id:long}")]
        public async Task<IActionResult> UpdateAttendanceDetailsAsync([FromBody] UpdateAttendanceRequestDto requestDto,
            long id)
        {
            var organizationId = (long)HttpContext.Items[Constants.Headers.OrganizationId]!;
            var result = await service.UpdateAttendanceDetailsAsync(requestDto, id, organizationId);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion

        #region DELETE Methods

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpDelete("Delete/{id:long}")]
        public async Task<IActionResult> DeleteAttendanceDetailsAsync(long id)
        {
            var organizationId = (long)HttpContext.Items[Constants.Headers.OrganizationId]!;
            var response = await service.DeleteAttendanceDetailsAsync(id, organizationId);
            return new ObjectResult(response.Response) { StatusCode = (int)response.StatusCode };
        }

        #endregion
    }
}