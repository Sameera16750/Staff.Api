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
    public class Organization(IOrganizationDetailService organizationDetailService) : ControllerBase
    {
        #region POST Methods

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpPost("save")]
        public async Task<IActionResult> SaveOrganizationAsync([FromBody] OrganizationRequestDto body)
        {
            var response = await organizationDetailService.SaveOrganizationAsync(body);
            return new ObjectResult(response.Response) { StatusCode = (int)response.StatusCode };
        }

        #endregion

        #region GET Methods

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizationDetailsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganizationByIdAsync(long id)
        {
            var response = await organizationDetailService.GetOrganizationByIdAsync(id, Constants.Status.Active);
            return new ObjectResult(response.Response) { StatusCode = (int)response.StatusCode };
        }

        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(PaginatedListResponseDto<OrganizationDetailsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpGet("list")]
        public async Task<IActionResult> GetOrganizationListAsync([FromQuery] PaginatedListRequestDto query)
        {
            var result = await organizationDetailService.GetAllOrganizationsAsync(
                pageNumber: query.PageNumber,
                pageSize: query.PageSize,
                search: query.SearchTerm, Constants.Status.Active);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion

        #region PUT Methods

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganizationAsync(long id, [FromBody] OrganizationRequestDto body)
        {
            var response = await organizationDetailService.UpdateOrganizationAsync(body, id);
            return new ObjectResult(response.Response) { StatusCode = (int)response.StatusCode };
        }

        #endregion

        #region DELETE Methods

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizationAsync(long id)
        {
            var response = await organizationDetailService.DeleteOrganizationAsync(id);
            return new ObjectResult(response.Response) { StatusCode = (int)response.StatusCode };
        }

        #endregion
    }
}