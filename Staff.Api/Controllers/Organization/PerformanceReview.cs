using Microsoft.AspNetCore.Mvc;
using Staff.Application.Models.Request.common;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Application.Models.Response.Organization;
using Staff.Application.Services.Interfaces.Organization;
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
        public async Task<IActionResult> SavePerformanceReview([FromBody] PerformanceReviewRequestDto request, [FromQuery] long organizationId)
        {
            var result = await performanceReviewService.SavePerformanceReview(request,organizationId);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion

        #region GET Methods

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PerformanceReviewResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpGet("Get/{id:long}")]
        public async Task<IActionResult> GetOrganizationByIdAsync(long id)
        {
            var response = await performanceReviewService.GetPerformanceReviewByIdAsync(id, new StatusDto());
            return new ObjectResult(response.Response) { StatusCode = (int)response.StatusCode };
        }

        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(PaginatedListResponseDto<PerformanceReviewResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetOrganizationListAsync([FromQuery] PerformanceReviewFilterDto filters)
        {
            var result = await performanceReviewService.GetAllPerformanceReviewsAsync(filters, new StatusDto());
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion
    }
}