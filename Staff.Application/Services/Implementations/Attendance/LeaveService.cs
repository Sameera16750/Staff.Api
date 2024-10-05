using Microsoft.Extensions.Logging;
using Staff.Application.Helpers.ResponseHelper;
using Staff.Application.Models.Request.Attendance;
using Staff.Application.Models.Response.Common;
using Staff.Application.Services.Interfaces.Attendance;
using Staff.Core.Constants;
using Staff.Infrastructure.Repositories.Interfaces.Attendance;

namespace Staff.Application.Services.Implementations.Attendance;

public class LeaveService(ILeveRepo leaveRepo, IResponseHelper responseHelper, ILogger<ILeaveService> logger)
    : ILeaveService
{
    #region POST Methods

    public async Task<ResponseWithCode<dynamic>> SaveLeaveType(SaveLeaveType leaveType, long organizationId)
    {
        try
        {
            logger.LogInformation("Checking available leave type");
            var existing =
                await leaveRepo.GetLeaveTypeByNameAndStatusAsync(leaveType.Type, Constants.Status.Active,
                    organizationId);
            if (existing != null)
            {
                logger.LogInformation($"{existing.Type} was already exist");
                return responseHelper.BadRequest(Constants.Messages.Error.LeaveTypeExist);
            }

            var result = await leaveRepo.SaveLeaveTypeAsync(leaveType.MapToEntity(leaveType,organizationId));
            return result == Constants.ProcessStatus.Failed
                ? responseHelper.SaveFailedResponse()
                : responseHelper.SaveSuccessResponse(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            return responseHelper.InternalServerErrorResponse();
        }
    }

    #endregion
}