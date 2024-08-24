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

    public async Task<ResponseWithCode<dynamic>> SaveDepartmentAsync(DepartmentRequestDto requestDto)
    {
        try
        {
            logger.LogInformation("Saving department");
            var org = await organizationDetailRepo.GetDetailsAsync(requestDto.Organization, Constants.Status.Active);
            if (org == null) return responseHelper.BadRequest(Constants.Messages.Error.InvalidOrganization);
            var existingDepartment = await departmentRepo
                .GetDepartmentByNameAsync(requestDto.Name, requestDto.Organization, Constants.Status.Active);
            if (existingDepartment == null) return responseHelper.BadRequest(Constants.Messages.Error.DepartmentExist);
            var department = requestDto.MapToEntity(requestDto, Constants.Status.Active);
            var result = await departmentRepo.SaveDepartmentAsync(department);
            return result == Constants.ProcessStatus.Failed
                ? responseHelper.SaveFailedResponse()
                : responseHelper.SaveSuccessResponse(result);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
            return responseHelper.InternalServerErrorResponse();
        }
    }

    #endregion

    #region GET Methods

    public async Task<ResponseWithCode<dynamic>> GetDepartmentAsync(long id)
    {
        try
        {
            var department = await departmentRepo.GetDepartmentAsync(id, Constants.Status.Active);
            if (department == null) return responseHelper.NotFoundErrorResponse();
            var response = new DepartmentResponseDto().MapToResponse(department);
            return responseHelper.CreateResponseWithCode<dynamic>(HttpStatusCode.OK, response);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
            return responseHelper.InternalServerErrorResponse();
        }
    }

    public async Task<ResponseWithCode<dynamic>> GetAllDepartmentsAsync(string search, int pageNumber, int pageSize,
        int departmentStatus, long organization, int organizationStatus)
    {
        try
        {
            logger.LogInformation("search departments processing ...");
            var result = await departmentRepo.GetAllDepartmentsAsync(search, pageNumber, pageSize, departmentStatus,
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

    public async Task<ResponseWithCode<dynamic>> UpdateDepartmentAsync(DepartmentRequestDto requestDto, long id)
    {
        try
        {
            logger.LogInformation("Updating department");
            var organization =
                await organizationDetailRepo.GetDetailsAsync(requestDto.Organization, Constants.Status.Active);
            if (organization == null)
            {
                return responseHelper.BadRequest(Constants.Messages.Error.InvalidOrganization);
            }

            var department = requestDto.MapToEntity(requestDto, Constants.Status.Active);
            department.Id = id;
            var result =
                await departmentRepo.UpdateDepartmentAsync(department);
            if (result == Constants.ProcessStatus.NotFound)
            {
                return responseHelper.BadRequest(Constants.Messages.Error.InvalidDepartment);
            }

            return result == Constants.ProcessStatus.Failed
                ? responseHelper.UpdateFailedResponse()
                : responseHelper.UpdateSuccessResponse(result);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
            return responseHelper.InternalServerErrorResponse();
        }
    }

    #endregion

    #region DELETE Methods

    public async Task<ResponseWithCode<dynamic>> DeleteDepartmentAsync(long id)
    {
        try
        {
            var result = await departmentRepo.DeleteDepartmentAsync(id);
            if (result == Constants.ProcessStatus.NotFound)
            {
                return responseHelper.BadRequest(Constants.Messages.Error.InvalidDepartment);
            }

            return result == Constants.ProcessStatus.Failed
                ? responseHelper.DeleteFailedErrorResponse()
                : responseHelper.DeleteSuccessResponse(result);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
            return responseHelper.InternalServerErrorResponse();
        }
    }

    #endregion
}