using Staff.Application.Models.Request.Company;
using Staff.Application.Models.Response.Common;

namespace Staff.Application.Services.Interfaces.Company;

public interface ICompanyDetailService
{
    Task<ResponseWithCode<dynamic>>SaveCompany(CompanyRequestDto company);
}