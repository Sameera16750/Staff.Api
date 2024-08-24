using Microsoft.Extensions.Logging;
using Staff.Application.Helpers.ResponseHelper;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
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
            var result = await designationRepo.SaveDesignationAsync(request.MapToEntity(request, Constants.Status.Active));
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
}