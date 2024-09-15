using System.Net;
using Microsoft.Extensions.Logging;
using Staff.Application.Helpers.DateFormatHelper;
using Staff.Application.Helpers.ResponseHelper;
using Staff.Application.Models.Request.Attendance;
using Staff.Application.Models.Request.common;
using Staff.Application.Models.Response.Attendance;
using Staff.Application.Models.Response.Common;
using Staff.Application.Services.Interfaces.Attendance;
using Staff.Core.Constants;
using Staff.Infrastructure.Models.Attendance;
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
            var exist = await attendanceDetailsRepo.GetAttendanceDetailsAsync(request.Date,
                request.StaffMemberId,
                Constants.Status.Active);
            long result;
            if (exist != null)
            {
                if (request.IsCheckIn) return responseHelper.BadRequest(Constants.Messages.Error.AlreadyCheckedIn);
                if (exist.CheckOut != null)
                    return responseHelper.BadRequest(Constants.Messages.Error.AlreadyCheckedOut);

                var dateValid = ValidateTimeRange(exist.Date, exist.CheckIn, request.CheckInCheckOutTime);

                if (dateValid != null) return dateValid;

                exist.CheckOut = request.CheckInCheckOutTime;
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
                if (!request.IsCheckIn) return responseHelper.BadRequest(Constants.Messages.Error.CheckInRequired);

                var dateValid = ValidateTimeRange(request.Date, request.CheckInCheckOutTime, null);
                if (dateValid != null) return dateValid;

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

    #region GET Methods

    public async Task<ResponseWithCode<dynamic>> GetAllAttendanceDetailsAsync(AttendanceFiltersDto filters,
        StatusDto status, long organizationId)
    {
        try
        {
            logger.LogInformation("Getting all Attendance Details ...");

            filters.FromDate = dateHelper.FormatDate(filters.FromDate);
            filters.ToDate = dateHelper.FormatDate(filters.ToDate);
            var attendance = await attendanceDetailsRepo.GetAllAttendanceDetailsAsync(filters, status, organizationId);
            var response = new PaginatedListResponseDto<AttendanceResponseDto>();
            if (attendance != null)
            {
                response = response.ToPaginatedListResponse(attendance,
                    new AttendanceResponseDto().MapToListResponse(attendance.Items));
            }

            return responseHelper.CreateResponseWithCode<dynamic>(HttpStatusCode.OK, response);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            return responseHelper.InternalServerErrorResponse();
        }
    }

    #endregion

    #region PUT Methods

    public async Task<ResponseWithCode<dynamic>> UpdateAttendanceDetailsAsync(UpdateAttendanceRequestDto request,
        long id,
        long organizationId)
    {
        try
        {
            logger.LogInformation("attendance request processing ...");
            var exist = await attendanceDetailsRepo.GetAttendanceDetailsByIdAsync(id, Constants.Status.Active,
                organizationId);
            if (exist != null)
            {
                logger.LogInformation("Updated attendance details ...");
                exist.CheckIn = dateHelper.FormatDate(request.CheckInTime);
                exist.CheckOut = dateHelper.FormatDate(request.CheckOutTime);
                var dateValid = ValidateTimeRange(exist.Date, exist.CheckIn, exist.CheckOut);
                if (dateValid != null) return dateValid;
                var result = await attendanceDetailsRepo.UpdateAttendanceDetailsAsync(exist);
                return result > 0
                    ? responseHelper.UpdateSuccessResponse(result)
                    : responseHelper.UpdateFailedResponse();
            }

            logger.LogError("attendance details not found");
            return responseHelper.BadRequest(Constants.Messages.Error.InvalidAttendance);
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
        request.Date = dateHelper.FormatDate(request.Date);
        request.CheckInCheckOutTime = dateHelper.FormatDate(request.CheckInCheckOutTime);
        return null;
    }

    private ResponseWithCode<dynamic>? ValidateTimeRange(DateTime date, DateTime? checkInTime, DateTime? checkOutTime)
    {
        if (checkInTime > checkOutTime)
        {
            logger.LogError("Provided check-in time invalid");
            return responseHelper.BadRequest(Constants.Messages.Error.InvalidCheckInTime);
        }

        if (!(date.Date > checkInTime?.Date) && !(date.Date > checkOutTime?.Date)) return null;
        logger.LogError("Provided check-in or check-out time invalid");
        return responseHelper.BadRequest(Constants.Messages.Error.InvalidCheckInCheckOutTime);
    }

    #endregion
}