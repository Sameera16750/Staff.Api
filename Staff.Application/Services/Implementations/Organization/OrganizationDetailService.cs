using System.Net;
using Microsoft.Extensions.Logging;
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
        #region POST Methods

        public async Task<ResponseWithCode<dynamic>> SaveOrganizationAsync(OrganizationRequestDto organization)
        {
            try
            {
                logger.LogInformation("Save organization processing..");

                OrganizationDetails newOrganization = organization.MapToEntity(organization, Constants.Status.Active);
                var result = await organizationDetailRepo.SaveOrganizationAsync(newOrganization);
                if (result > 0)
                {
                    return responseHelper.SaveSuccessResponse(result);
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

                if (result != null)
                {
                    OrganizationDetailsResponseDto dto = new OrganizationDetailsResponseDto().MapToResponse(result);
                    return responseHelper.CreateResponseWithCode<dynamic>(HttpStatusCode.OK, dto);
                }

                return responseHelper.NotFoundErrorResponse();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return responseHelper.InternalServerErrorResponse();
            }
        }

        public async Task<ResponseWithCode<dynamic>> GetAllOrganizationsAsync(int pageNumber, int pageSize, string search,
            int status)
        {
            try
            {
                logger.LogInformation("Search organizations processing..");
                var result = await organizationDetailRepo.GetAllOrganizationsAsync(search: search, pageNumber: pageNumber,
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

        public async Task<ResponseWithCode<dynamic>> UpdateOrganizationAsync(OrganizationRequestDto organization, long id)
        {
            try
            {
                logger.LogInformation("Update organization processing..");

                OrganizationDetails newOrganization = organization.MapToEntity(organization, Constants.Status.Active);
                newOrganization.Id = id;
                var result = await organizationDetailRepo.UpdateOrganizationAsync(newOrganization);
                if (result > 0)
                {
                    return responseHelper.UpdateSuccessResponse(result);
                }

                return responseHelper.UpdateFailedResponse();
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