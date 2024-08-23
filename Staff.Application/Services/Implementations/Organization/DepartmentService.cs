using System.Net;
using Microsoft.Extensions.Logging;
using Staff.Application.Helpers.ResponseHelper;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Application.Models.Response.Organization;
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
    #region POST Methods

    public async Task<ResponseWithCode<dynamic>> SaveDepartment(DepartmentRequestDto requestDto)
    {
        try
        {
            logger.LogInformation("Saving department");
            var org = await organizationDetailRepo.GetDetails(requestDto.Organization, Constants.Status.Active);
            if (org != null)
            {
                var department = requestDto.MapToEntity(requestDto, Constants.Status.Active);
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

    #endregion

    #region GET Methods

    public async Task<ResponseWithCode<dynamic>> GetDepartment(long id)
    {
        try
        {
            var department = await departmentRepo.GetDepartment(id, Constants.Status.Active);
            if (department != null)
            {
                var response = new DepartmentResponseDto().MapToResponse(department);
                return responseHelper.CreateResponseWithCode<dynamic>(HttpStatusCode.OK, response);
            }

            return responseHelper.NotFoundErrorResponse();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
            return responseHelper.InternalServerErrorResponse();
        }
    }

    public async Task<ResponseWithCode<dynamic>> GetAllDepartments(string search, int pageNumber, int pageSize,
        int departmentStatus, long organization, int organizationStatus)
    {
        try
        {
            logger.LogInformation("search departments processing ...");
            var result = await departmentRepo.GetAllDepartments(search, pageNumber, pageSize, departmentStatus,
                organization, organizationStatus);
            var response = new PaginatedListResponseDto<DepartmentResponseDto>();
            if (result != null)
            {
                response = response.ToPaginatedListResponse(
                    result,
                    new DepartmentResponseDto().MapToResponseList(result.Items)
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

    public async Task<ResponseWithCode<dynamic>> UpdateDepartment(DepartmentRequestDto requestDto, long id)
    {
        try
        {
            logger.LogInformation("Updating department");
            var organization =
                await organizationDetailRepo.GetDetails(requestDto.Organization, Constants.Status.Active);
            if (organization == null)
            {
                return responseHelper.BadRequest(Constants.Messages.Error.InvalidOrganization);
            }

            var department = requestDto.MapToEntity(requestDto, Constants.Status.Active);
            department.Id = id;
            var result =
                await departmentRepo.UpdateDepartment(department);
            if (result == Constants.ProcessStatus.NotFound)
            {
                return responseHelper.BadRequest(Constants.Messages.Error.InvalidDepartment);
            }

            if (result == Constants.ProcessStatus.Failed)
            {
                return responseHelper.UpdateFailedResponse();
            }

            return responseHelper.UpdateSuccessResponse(result);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
            return responseHelper.InternalServerErrorResponse();
        }
    }

    #endregion

    #region DELETE Methods

    public async Task<ResponseWithCode<dynamic>> DeleteDepartment(long id)
    {
        try
        {
            var result = await departmentRepo.DeleteDepartment(id);
            if (result == Constants.ProcessStatus.NotFound)
            {
                return responseHelper.BadRequest(Constants.Messages.Error.InvalidDepartment);
            }

            if (result == Constants.ProcessStatus.Failed)
            {
                return responseHelper.DeleteFailedErrorResponse();
            }

            return responseHelper.DeleteSuccessResponse(result);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
            return responseHelper.InternalServerErrorResponse();
        }
    }

    #endregion
}