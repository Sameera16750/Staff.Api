using System.Net;
using Microsoft.Extensions.Logging;
using Staff.Application.Helpers.ResourceHelper;
using Staff.Application.Helpers.ResponseHelper;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Application.Models.Response.Organization;
using Staff.Application.Services.Interfaces.Organization;
using Staff.Core.Constants;
using Staff.Core.Entities.Organization;
using Staff.Infrastructure.Repositories.Interfaces.Organization;

namespace Staff.Application.Services.Implementations.Organization
{
    public class OrganizationDetailService(
        IOrganizationDetailRepo organizationDetailRepo,
        ILogger<IOrganizationDetailService> logger,
        IResponseHelper responseHelper) : IOrganizationDetailService
    {
        public async Task<ResponseWithCode<dynamic>> SaveCompany(OrganizationRequestDto organization)
        {
            try
            {
                logger.LogInformation("Save organization processing..");

                OrganizationDetails newOrganization = organization.MapToEntity(organization, 1);
                var result = await organizationDetailRepo.SaveCompany(newOrganization);
                if (result > 0)
                {
                    var response = responseHelper.CreateIdResponse(
                        result,
                        Constants.Messages.Success.SaveSuccess
                    );
                    return responseHelper.CreateResponseWithCode<dynamic>(
                        HttpStatusCode.Created,
                        response
                    );
                }
                else
                {
                    var response = responseHelper.CreateMessageResponse(
                        Constants.Messages.Error.SaveFailed
                    );

                    return responseHelper.CreateResponseWithCode<dynamic>(
                        HttpStatusCode.InternalServerError,
                        response
                    );
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                var response = responseHelper.CreateMessageResponse(
                    Constants.Messages.Error.InternalServerError
                );

                return responseHelper.CreateResponseWithCode<dynamic>(
                    HttpStatusCode.InternalServerError,
                    response
                );
            }
        }

        public async Task<ResponseWithCode<dynamic>> GetOrganizationById(long id)
        {
            try
            {
                logger.LogInformation("Find organization processing..");
                var result = await organizationDetailRepo.GetDetails(id);

                if (result != null)
                {
                    OrganizationDetailsResponseDto dto = new OrganizationDetailsResponseDto().MapToResponse(result);
                    return responseHelper.CreateResponseWithCode<dynamic>(HttpStatusCode.OK, dto);
                }

                return responseHelper.CreateResponseWithCode<dynamic>(HttpStatusCode.NotFound,
                    responseHelper.CreateMessageResponse(Constants.Messages.Error.DataNotFound));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return responseHelper.CreateResponseWithCode<dynamic>(HttpStatusCode.InternalServerError,
                    responseHelper.CreateMessageResponse(Constants.Messages.Error.InternalServerError));
            }
        }
    }
}