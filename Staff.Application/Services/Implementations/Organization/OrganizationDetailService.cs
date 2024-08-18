using System.Net;
using Microsoft.Extensions.Logging;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Application.Services.Interfaces.Organization;
using Staff.Core.Entities.Organization;
using Staff.Infrastructure.Repositories.Interfaces.Organization;

namespace Staff.Application.Services.Implementations.Organization
{
    public class OrganizationDetailService(IOrganizationDetailRepo organizationDetailRepo, ILogger<IOrganizationDetailService> logger) : IOrganizationDetailService
    {
        private readonly IOrganizationDetailRepo _organizationDetailRepo = organizationDetailRepo;
        private readonly ILogger<IOrganizationDetailService> _logger = logger;

        public async Task<ResponseWithCode<dynamic>> SaveCompany(OrganizationRequestDto organization)
        {
            try
            {
                _logger.LogInformation("Saving company processing..");
                
                OrganizationDetails newOrganization=organization.MapToEntity(organization,1);
                var result=await _organizationDetailRepo.SaveCompany(newOrganization);
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