using Microsoft.AspNetCore.Mvc;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Application.Services.Interfaces.Organization;

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
            var result = await performanceReviewService.SavePerformanceReview(request);
            return new ObjectResult(result.Response) { StatusCode = (int)result.StatusCode };
        }

        #endregion
    }
}