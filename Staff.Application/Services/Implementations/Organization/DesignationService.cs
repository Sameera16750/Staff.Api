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

    #region PUT Methods

    public async Task<ResponseWithCode<dynamic>> UpdateDesignationAsync(DesignationRequestDto request, long id)
    {
        try
        {
            var validate = await ValidateDesignationRequest(request, id);
            if (validate != null) return validate;
            logger.LogInformation("Update designation processing ...");
            var updated = request.MapToEntity(request, Constants.Status.Active);
            updated.Id = id;
            var result =
                await designationRepo.UpdateDesignationAsync(updated);
            return result == Constants.ProcessStatus.NotFound
                ? responseHelper.BadRequest(Constants.Messages.Error.InvalidDesignation)
                : result == Constants.ProcessStatus.Failed
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

    #region Private Methods

    private async Task<ResponseWithCode<dynamic>?> ValidateDesignationRequest(DesignationRequestDto request, long id)
    {
        logger.LogInformation("Starting to validate designation ...");

        var department = await departmentRepo.GetDepartmentAsync(request.DepartmentId, Constants.Status.Active);
        if (department == null) return responseHelper.BadRequest(Constants.Messages.Error.InvalidDepartment);
        var designation =
            await designationRepo.GetDesignationByNameAsync(request.Name, request.DepartmentId,
                Constants.Status.Active);
        if (designation != null && (id > 0 && id != designation.Id))
            return responseHelper.BadRequest(Constants.Messages.Error.DesignationExists);

        logger.LogInformation("Validation completed ...");
        return null;
    }

    #endregion
}