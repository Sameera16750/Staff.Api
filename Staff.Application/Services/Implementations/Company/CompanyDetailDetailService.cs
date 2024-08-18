using System.Net;
using Microsoft.Extensions.Logging;
using Staff.Application.Models.Request.Company;
using Staff.Application.Models.Response.Common;
using Staff.Application.Services.Interfaces.Company;
using Staff.Core.Entities.Company;
using Staff.Infrastructure.Repositories.Interfaces.company;

namespace Staff.Application.Services.Implementations.Company
{
    public class CompanyDetailDetailService(ICompanyDetailRepo companyDetailRepo, ILogger<ICompanyDetailService> logger) : ICompanyDetailService
    {
        private readonly ICompanyDetailRepo _companyDetailRepo = companyDetailRepo;
        private readonly ILogger<ICompanyDetailService> _logger = logger;

        public async Task<ResponseWithCode<dynamic>> SaveCompany(CompanyRequestDto company)
        {
            try
            {
                _logger.LogInformation("Saving company processing..");
                
                CompanyDetails newCompany=company.MapToEntity(company,1);
                var result=await _companyDetailRepo.SaveCompany(newCompany);
                if (result>0)
                {
                    var response = new IdResponse<long>
                    {
                        Id = result,
                        Message = "Company Saving Successful"
                    };
                    return new ResponseWithCode<dynamic>
                    {
                        Response = response,
                        StatusCode = HttpStatusCode.OK
                    };   
                }
                else
                {
                    var response = new MessageResponse
                    {
                        Message = "Company Saving Failed",
                    };
                    return new ResponseWithCode<dynamic>
                    {
                        Response = response,
                        StatusCode = HttpStatusCode.InternalServerError
                    };
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                var response = new MessageResponse { Message = "Failed to save company." };
                return new ResponseWithCode<dynamic>
                    { Response = response, StatusCode = HttpStatusCode.InternalServerError };
            }
        }
    }
}