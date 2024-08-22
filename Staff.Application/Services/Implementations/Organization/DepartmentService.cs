using Microsoft.Extensions.Logging;
using Staff.Application.Helpers.ResponseHelper;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Application.Services.Interfaces.Organization;
using Staff.Core.Constants;
using Staff.Infrastructure.Repositories.Interfaces.Organization;

namespace Staff.Application.Services.Implementations.Organization;

public class DepartmentService(
    IDepartmentRepo departmentRepo,
    IOrganizationDetailRepo organizationDetailRepo,
    ILogger<IDepartmentService> logger,
    IResponseHelper responseHelper)
    : IDepartmentService
{
    public async Task<ResponseWithCode<dynamic>> SaveDepartment(DepartmentRequestDto requestDto)
    {
        try
        {
            logger.LogInformation("Saving department");
            var org = await organizationDetailRepo.GetDetails(requestDto.Organization, Constants.Status.Active);
            if (org != null)
            {
                var department = requestDto.MapToEntity(requestDto, org, Constants.Status.Active);
                var result = await departmentRepo.SaveDepartment(department);
                if (result > 0)
                {
                    return responseHelper.SaveSuccessResponse(result);
                }

                return responseHelper.SaveFailedResponse();
            }

            return responseHelper.BadRequest(Constants.Messages.Error.InvalidOrganization);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
            return responseHelper.InternalServerErrorResponse();
        }
    }
}