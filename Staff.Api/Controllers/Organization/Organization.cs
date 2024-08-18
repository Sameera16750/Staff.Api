using Microsoft.AspNetCore.Mvc;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Application.Services.Interfaces.Organization;

namespace Staff.Api.Controllers.Organization
{
    [Route("api/[controller]")]
    [ApiController]
    public class Organization (IOrganizationDetailService organizationDetailService): ControllerBase
    {
        private readonly IOrganizationDetailService _organizationDetailService = organizationDetailService;

        #region Company

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdResponse<int>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpPost("save")]
        public async Task<IActionResult> SaveCompany([FromBody] OrganizationRequestDto body)
        {
            var response = await _organizationDetailService.SaveCompany(body);
            return new ObjectResult(response.Response) { StatusCode = (int)response.StatusCode };
        }

        #endregion
    }
}
