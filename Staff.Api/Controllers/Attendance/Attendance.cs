using Microsoft.AspNetCore.Mvc;
using Staff.Application.Models.Request.Attendance;
using Staff.Application.Models.Request.common;
using Staff.Application.Models.Response.Attendance;
using Staff.Application.Models.Response.Common;
using Staff.Application.Services.Interfaces.Attendance;
using Staff.Core.Constants;
using Staff.Infrastructure.Models.Attendance;

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
    }
}