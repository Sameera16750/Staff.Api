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

public class DesignationService(
    IDepartmentRepo departmentRepo,
    IDesignationRepo designationRepo,
    IResponseHelper responseHelper,
    ILogger<IDesignationService> logger)
    : IDesignationService
{
    #region POST Methods

    public async Task<ResponseWithCode<dynamic>> SaveDesignationAsync(DesignationRequestDto request)
    {
        try
        {
            logger.LogInformation("Save designation processing ...");
            var department = await departmentRepo.GetDepartmentAsync(request.DepartmentId, Constants.Status.Active);
            if (department == null) return responseHelper.BadRequest(Constants.Messages.Error.InvalidDepartment);
            var designation =
                await designationRepo.GetDesignationByNameAsync(request.Name, request.DepartmentId,
                    Constants.Status.Active);
            if (designation != null) return responseHelper.BadRequest(Constants.Messages.Error.DesignationExists);
            var result =
                await designationRepo.SaveDesignationAsync(request.MapToEntity(request, Constants.Status.Active));
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

    public async Task<ResponseWithCode<dynamic>> GetDesignationByIdAsync(long id)
    {
        try
        {
            logger.LogInformation("Get designation processing ...");
            var designation = await designationRepo.GetDesignationByIdAsync(id, Constants.Status.Active);
            if (designation == null) return responseHelper.NotFoundErrorResponse();
            var response = new DesignationResponseDto().MapToResponse(designation);
            return responseHelper.CreateResponseWithCode<dynamic>(HttpStatusCode.OK, response);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
            return responseHelper.InternalServerErrorResponse();
        }
    }

    public async Task<ResponseWithCode<dynamic>> GetAllDesignationAsync(string search, int pageNumber, int pageSize,
        int designationStatus, long department,
        int departmentStatus, int organizationStatus)
    {
        try
        {
            logger.LogInformation("GetAll designation processing ...");
            var designations = await designationRepo.GetAllDesignationsAsync(
                search: search,
                pageNumber: pageNumber,
                pageSize: pageSize,
                designationStatus: designationStatus,
                department: department,
                departmentStatus: departmentStatus,
                organizationStatus: organizationStatus
            );
            var response = new PaginatedListResponseDto<DesignationResponseDto>();
            if (designations != null)
            {
                response = response.ToPaginatedListResponse(
                    designations,
                    new DesignationResponseDto().MapToListResponse(designations.Items)
                );
            }

            return responseHelper.CreateResponseWithCode<dynamic>(HttpStatusCode.OK, response);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
            return responseHelper.InternalServerErrorResponse();
        }
    }

    #endregion
}