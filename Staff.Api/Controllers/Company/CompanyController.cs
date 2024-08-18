using System.Net;
using Microsoft.AspNetCore.Mvc;
using Staff.Application.Models.Request.Company;
using Staff.Application.Models.Response.Common;
using Staff.Application.Services.Interfaces.Company;

namespace Staff.Api.Controllers.Company
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController(ICompanyDetailService companyDetailService) : ControllerBase
    {
        private readonly ICompanyDetailService _companyDetailService = companyDetailService;

        #region Company

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdResponse<int>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MessageResponse))]
        [HttpPost("save")]
        public async Task<IActionResult> SaveCompany([FromBody] CompanyRequestDto body)
        {
            var response = await _companyDetailService.SaveCompany(body);
            return new ObjectResult(response.Response) { StatusCode = (int)response.StatusCode };
        }

        #endregion
    }
}