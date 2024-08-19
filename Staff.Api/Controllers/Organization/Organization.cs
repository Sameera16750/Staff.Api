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
        #region Organization

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpPost("save")]
        public async Task<IActionResult> SaveOrganization([FromBody] OrganizationRequestDto body)
        {
            var response = await organizationDetailService.SaveOrganization(body);
            return new ObjectResult(response.Response) { StatusCode = (int)response.StatusCode };
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizationDetailsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganizationById(long id)
        {
            var response = await organizationDetailService.GetOrganizationById(id,Constants.Status.Active);
            return new ObjectResult(response.Response) { StatusCode = (int)response.StatusCode };
        }


        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(PaginatedListResponseDto<OrganizationDetailsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpGet("list")]
        public async Task<IActionResult> GetOrganizationList([FromQuery] PaginatedListRequestDto query)
        {
            var result = await organizationDetailService.GetAllOrganizations(
                pageNumber: query.PageNumber,
                pageSize: query.PageSize,
                search: query.SearchTerm,Constants.Status.Active);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganization(long id, [FromBody] OrganizationRequestDto body)
        {
            var response = await organizationDetailService.UpdateOrganization(body, id);
            return new ObjectResult(response.Response) { StatusCode = (int)response.StatusCode };
        }

        #endregion
        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(long id)
        {
            var response = await organizationDetailService.DeleteOrganization( id);
            return new ObjectResult(response.Response) { StatusCode = (int)response.StatusCode };
        }
        
    }
}