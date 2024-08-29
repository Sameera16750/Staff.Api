using Microsoft.AspNetCore.Mvc;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Application.Models.Response.Organization;
using Staff.Application.Services.Interfaces.Organization;
using Staff.Core.Constants;

namespace Staff.Api.Controllers.Organization
{
    [Route("api/[controller]")]
    [ApiController]
    public class Staff(IStaffMemberService staffMemberService) : ControllerBase
    {
        #region POST Methods

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpPost("Save")]
        public async Task<IActionResult> SaveStaff([FromBody] StaffMemberRequestDto requestDto)
        {
            var result = await staffMemberService.SaveStaffMemberAsync(requestDto);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion

        #region GET Methods

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StaffMemberResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpGet("Get/{id:long}")]
        public async Task<IActionResult> GetStaffMemberByIdAsync(long id)
        {
            var result = await staffMemberService.GetStaffMemberByIdAsync(id, Constants.Status.Active);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion

        #region PUT Methods

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpPut("Update/{id:long}")]
        public async Task<IActionResult> UpdateStaffMemberAsync([FromBody] StaffMemberRequestDto requestDto,long id)
        {
            var result=await staffMemberService.UpdateStaffMemberAsync(requestDto,id);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion
    }
}