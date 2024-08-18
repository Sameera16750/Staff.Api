using Microsoft.AspNetCore.Mvc;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Application.Models.Response.Organization;
using Staff.Application.Services.Interfaces.Organization;

namespace Staff.Api.Controllers.Organization
{
    [Route("api/[controller]")]
    [ApiController]
    public class Organization(IOrganizationDetailService organizationDetailService) : ControllerBase
    {
        #region Organization

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpPost("save")]
        public async Task<IActionResult> SaveOrganization([FromBody] OrganizationRequestDto body)
        {
            var response = await organizationDetailService.SaveCompany(body);
            return new ObjectResult(response.Response) { StatusCode = (int)response.StatusCode };
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizationDetailsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganizationById(long id)
        {
            var response = await organizationDetailService.GetOrganizationById(id);
            return new ObjectResult(response.Response) { StatusCode = (int)response.StatusCode };
        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedListResponseDto<OrganizationDetailsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpGet("list")]
        public async Task<IActionResult> GetOrganizationList([FromQuery] OrganizationListRequestDto query)
        {
            var result = await organizationDetailService.GetAllOrganizations(
                pageNumber: query.PageNumber,
                pageSize: query.PageSize,
                search: query.SearchTerm);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion
    }
}