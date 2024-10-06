using Microsoft.AspNetCore.Mvc;
using Staff.Application.Models.Request.common;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Application.Models.Response.Organization;
using Staff.Application.Services.Interfaces.Organization;
using Staff.Core.Constants;
using Staff.Infrastructure.Models.Common;
using Staff.Infrastructure.Models.Staff;

namespace Staff.Api.Controllers.Organization
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceReview(IPerformanceReviewService performanceReviewService) : ControllerBase
    {
        #region POST Methods

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpPost("Save")]
        public async Task<IActionResult> SavePerformanceReview([FromBody] PerformanceReviewRequestDto request)
        {
            var organizationId = (long)HttpContext.Items[Constants.Headers.OrganizationId]!;
            var result = await performanceReviewService.SavePerformanceReview(request, organizationId);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion

        #region GET Methods

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PerformanceReviewResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpGet("Get/{id:long}")]
        public async Task<IActionResult> GetReviewByIdAsync(long id)
        {
            var organizationId = (long)HttpContext.Items[Constants.Headers.OrganizationId]!;
            var response =
                await performanceReviewService.GetPerformanceReviewByIdAsync(id, organizationId, new StatusDto());
            return new ObjectResult(response.Response) { StatusCode = (int)response.StatusCode };
        }

        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(PaginatedListResponseDto<PerformanceReviewResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetReviewListAsync([FromQuery] PerformanceReviewFilterDto filters)
        {
            var organizationId = (long)HttpContext.Items[Constants.Headers.OrganizationId]!;
            var result =
                await performanceReviewService.GetAllPerformanceReviewsAsync(filters, organizationId, new StatusDto());
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion

        #region PUT Methods

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpPut("Update/{id:long}")]
        public async Task<IActionResult> UpdateReviewAsync([FromBody] PerformanceReviewRequestDto requestDto,
            long id)
        {
            var organizationId = (long)HttpContext.Items[Constants.Headers.OrganizationId]!;
            var result = await performanceReviewService.UpdatePerformanceReviewAsync(requestDto, id, organizationId);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion

        #region DELETE Methods

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdResponse<long>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpDelete("Delete/{id:long}")]
        public async Task<IActionResult> DeleteStaffMemberAsync(long id)
        {
            var organizationId = (long)HttpContext.Items[Constants.Headers.OrganizationId]!;
            var response = await performanceReviewService.DeletePerformanceReviewAsync(id, organizationId);
            return new ObjectResult(response.Response) { StatusCode = (int)response.StatusCode };
        }

        #endregion
    }
}