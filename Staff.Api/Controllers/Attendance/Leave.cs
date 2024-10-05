using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Staff.Application.Models.Request.Attendance;
using Staff.Application.Models.Response.Common;
using Staff.Application.Services.Interfaces.Attendance;
using Staff.Core.Constants;

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
            var result = await service.SaveLeaveType(requestDto, organizationId);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion
    }
}