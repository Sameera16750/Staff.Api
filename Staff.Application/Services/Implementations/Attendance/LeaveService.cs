using System.Net;
using Microsoft.Extensions.Logging;
using Staff.Application.Helpers.ResponseHelper;
using Staff.Application.Models.Request.Attendance;
using Staff.Application.Models.Response.Attendance;
using Staff.Application.Models.Response.Common;
using Staff.Application.Services.Interfaces.Attendance;
using Staff.Core.Constants;
using Staff.Infrastructure.Models.Common;
using Staff.Infrastructure.Repositories.Interfaces.Attendance;

namespace Staff.Application.Services.Implementations.Attendance;

public class LeaveService(ILeveRepo leaveRepo, IResponseHelper responseHelper, ILogger<ILeaveService> logger)
    : ILeaveService
{
    #region POST Methods

    public async Task<ResponseWithCode<dynamic>> SaveLeaveTypeAsync(SaveLeaveType leaveType, long organizationId)
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

            var result = await leaveRepo.SaveLeaveTypeAsync(leaveType.MapToEntity(leaveType, organizationId));
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

    #region GET Methods

    public async Task<ResponseWithCode<dynamic>> GetLeaveTypeAsync(long id, long organizationId)
    {
        try
        {
            logger.LogInformation($"processing get leave type by {id} and organization id {organizationId}");
            var leaveType = await leaveRepo.GetLeaveTypeByIdAsync(id, Constants.Status.Active, organizationId);
            if (leaveType is not null)
                return responseHelper.CreateResponseWithCode<dynamic>(HttpStatusCode.OK,
                    new LeaveTypeResponseDto().MapToResponse(leaveType));
            logger.LogInformation($"leave type was not found with this id {id}");
            return responseHelper.NotFoundErrorResponse();
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            return responseHelper.InternalServerErrorResponse();
        }
    }

    public async Task<ResponseWithCode<dynamic>> GetAllLeaveTypesAsync(PaginationDto filters, StatusDto status,
        long organizationId)
    {
        try
        {
            logger.LogInformation(
                $"processing get leave types by organization id {organizationId} search filters {filters} and leave type status {status.LeaveType}");
            var leaveTypes = await leaveRepo.GetAllLeaveTypesAsync(filters, status, organizationId);
            var response = new PaginatedListResponseDto<LeaveTypeResponseDto>();
            if (leaveTypes is not null)
            {
                response = response.ToPaginatedListResponse(leaveTypes,
                    new LeaveTypeResponseDto().MapToResponseList(leaveTypes.Items));
            }

            logger.LogInformation("Leave types not found");
            return responseHelper.CreateResponseWithCode<dynamic>(HttpStatusCode.OK, response);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            return responseHelper.InternalServerErrorResponse();
        }
    }

    #endregion
}