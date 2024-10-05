using System.Net;
using Microsoft.Extensions.Logging;
using Staff.Application.Helpers.ResponseHelper;
using Staff.Application.Helpers.SecurityHelper;
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
        IResponseHelper responseHelper,
        ISecurityHelper securityHelper) : IOrganizationDetailService
    {
        #region POST Methods

        public async Task<ResponseWithCode<dynamic>> SaveOrganizationAsync(OrganizationRequestDto organization)
        {
            try
            {
                logger.LogInformation("Save organization processing..");
                if (organization.ExpireDate.Kind != DateTimeKind.Utc)
                {
                    organization.ExpireDate = organization.ExpireDate.ToUniversalTime();
                }

                var apiKey = securityHelper.GenerateApiToken(organization.Name);
                OrganizationDetails newOrganization =
                    organization.MapToEntity(organization, apiKey, Constants.Status.Active);
                var result = await organizationDetailRepo.SaveOrganizationAsync(newOrganization);
                if (result > 0)
                {
                    return responseHelper.CreateResponseWithCode<dynamic>(HttpStatusCode.Created, new IdResponse<string>
                    {
                        Id = apiKey,
                        Message = "Organization Creation successfully, Use this ID as api key"
                    });
                }

                return responseHelper.SaveFailedResponse();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return responseHelper.InternalServerErrorResponse();
            }
        }

        #endregion

        #region GET Methods

        public async Task<ResponseWithCode<dynamic>> GetOrganizationByIdAsync(long id, int status)
        {
            try
            {
                logger.LogInformation("Find organization processing..");
                var result = await organizationDetailRepo.GetDetailsAsync(id, status);

                if (result == null) return responseHelper.NotFoundErrorResponse();
                var dto = new OrganizationDetailsResponseDto().MapToResponse(result);
                return responseHelper.CreateResponseWithCode<dynamic>(HttpStatusCode.OK, dto);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return responseHelper.InternalServerErrorResponse();
            }
        }

        public async Task<ResponseWithCode<dynamic>> GetAllOrganizationsAsync(int pageNumber, int pageSize,
            string search,
            int status)
        {
            try
            {
                logger.LogInformation("Search organizations processing..");
                var result = await organizationDetailRepo.GetAllOrganizationsAsync(search: search,
                    pageNumber: pageNumber,
                    pageSize: pageSize, status: status);
                var response = new PaginatedListResponseDto<OrganizationDetailsResponseDto>();
                if (result != null)
                {
                    response = response.ToPaginatedListResponse(
                        result,
                        new OrganizationDetailsResponseDto().MapToResponseList(result.Items)
                    );
                }

                return responseHelper.CreateResponseWithCode<dynamic>(
                    HttpStatusCode.OK,
                    response
                );
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return responseHelper.InternalServerErrorResponse();
            }
        }

        #endregion

        #region PUT Methods

        public async Task<ResponseWithCode<dynamic>> UpdateOrganizationAsync(OrganizationRequestDto organization,
            long id, bool updateApikey)
        {
            try
            {
                logger.LogInformation("Update organization processing..");

                if (organization.ExpireDate.Kind != DateTimeKind.Utc)
                {
                    organization.ExpireDate = organization.ExpireDate.ToUniversalTime();
                }

                var apiKey = "";
                if (updateApikey)
                {
                    apiKey = securityHelper.GenerateApiToken(organization.Name);
                }

                OrganizationDetails newOrganization =
                    organization.MapToEntity(organization, apiKey, Constants.Status.Active);
                newOrganization.Id = id;
                var result = await organizationDetailRepo.UpdateOrganizationAsync(newOrganization, updateApikey);
                if (result > 0)
                {
                    return updateApikey
                        ? responseHelper.CreateResponseWithCode<dynamic>(HttpStatusCode.Created, new IdResponse<string>
                        {
                            Id = apiKey,
                            Message = "Organization update successfully, Use this ID as api key"
                        })
                        : responseHelper.UpdateSuccessResponse(result);
                }

                return result == Constants.ProcessStatus.NotFound
                    ? responseHelper.BadRequest(Constants.Messages.Error.InvalidOrganization)
                    : responseHelper.UpdateFailedResponse();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return responseHelper.InternalServerErrorResponse();
            }
        }

        #endregion

        #region DELETE Methods

        public async Task<ResponseWithCode<dynamic>> DeleteOrganizationAsync(long id)
        {
            try
            {
                logger.LogInformation("Delete organization processing..");
                var org = await organizationDetailRepo.GetDetailsAsync(id, Constants.Status.Active);
                if (org != null && org.Status != Constants.Status.Deleted)
                {
                    org.Status = Constants.Status.Deleted;
                    var result = await organizationDetailRepo.UpdateOrganizationAsync(org);
                    if (result < 1)
                    {
                        return responseHelper.DeleteFailedErrorResponse();
                    }

                    return responseHelper.DeleteSuccessResponse(result);
                }

                return responseHelper.NotFoundErrorResponse();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return responseHelper.InternalServerErrorResponse();
            }
        }

        #endregion
    }
}