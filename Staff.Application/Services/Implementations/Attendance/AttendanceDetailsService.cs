using System.Net;
using Microsoft.Extensions.Logging;
using Staff.Application.Helpers.DateFormatHelper;
using Staff.Application.Helpers.ResponseHelper;
using Staff.Application.Models.Request.Attendance;
using Staff.Application.Models.Response.Common;
using Staff.Application.Services.Interfaces.Attendance;
using Staff.Core.Constants;
using Staff.Infrastructure.Repositories.Interfaces.Attendance;
using Staff.Infrastructure.Repositories.Interfaces.Organization;

namespace Staff.Application.Services.Implementations.Attendance;

public class AttendanceDetailsService(
    IAttendanceDetailsRepo attendanceDetailsRepo,
    IStaffMemberRepo staffMemberRepo,
    IResponseHelper responseHelper,
    ILogger<IAttendanceDetailsService> logger,
    IDateFormatHelper dateHelper
) : IAttendanceDetailsService
{
    #region POST Methods

    public async Task<ResponseWithCode<dynamic>> SaveAttendanceAsync(SaveAttendanceRequestDto request,
        long organizationId)
    {
        try
        {
            logger.LogInformation("attendance request processing ...");
            var validation = await ValidateRequest(request, organizationId);
            if (validation != null) return validation;
            var exist = await attendanceDetailsRepo.GetAttendanceDetailsAsync(request.DateAndTime,
                request.StaffMemberId,
                Constants.Status.Active);
            long result;
            if (exist != null)
            {
                exist.CheckOut = request.DateAndTime;
                result = await attendanceDetailsRepo.UpdateAttendanceDetailsAsync(exist);
                var message = result > 0
                    ? Constants.Messages.Success.CheckOtSuccess
                    : Constants.Messages.Error.CheckOtFailed;
                var code = result > 0 ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
                return responseHelper.CreateResponseWithCode<dynamic>(code,
                    responseHelper.CreateMessageResponse(message));
            }
            else
            {
                result = await attendanceDetailsRepo.SaveAttendanceDetailsAsync(request.MapToEntity(request));
                var message = result > 0
                    ? Constants.Messages.Success.CheckInSuccess
                    : Constants.Messages.Error.CheckInFailed;
                var code = result > 0 ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
                return responseHelper.CreateResponseWithCode<dynamic>(code,
                    responseHelper.CreateMessageResponse(message));
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            return responseHelper.InternalServerErrorResponse();
        }
    }

    #endregion

    #region Private Methods

    private async Task<ResponseWithCode<dynamic>?> ValidateRequest(SaveAttendanceRequestDto request,
        long organizationId)
    {
        logger.LogInformation("validating attendance request ...");
        logger.LogInformation("checking available staff members");
        var staffMember = await staffMemberRepo.GetStaffMemberByIdAsync(
            id: request.StaffMemberId,
            organizationId: organizationId,
            status: Constants.Status.Active
        );
        if (staffMember == null)
        {
            logger.LogError("staff member not found");
            return responseHelper.BadRequest(Constants.Messages.Error.InvalidStaff);
        }

        logger.LogInformation("staff member validated");
        request.DateAndTime = dateHelper.FormatDate(request.DateAndTime);
        return null;
    }

    #endregion
}