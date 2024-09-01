using Microsoft.Extensions.Logging;
using Staff.Application.Helpers.ResponseHelper;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Application.Services.Interfaces.Organization;
using Staff.Core.Constants;
using Staff.Infrastructure.Repositories.Interfaces.Organization;

namespace Staff.Application.Services.Implementations.Organization;

public class PerformanceReviewService(
    IPerformanceReviewRepo performanceReviewRepo,
    IStaffMemberRepo staffMemberRepo,
    IResponseHelper responseHelper,
    ILogger<IPerformanceReviewService> logger) : IPerformanceReviewService
{
    #region POST Methods

    public async Task<ResponseWithCode<dynamic>> SavePerformanceReview(PerformanceReviewRequestDto request)
    {
        try
        {
            logger.LogInformation("Saving performance review ...");
            var validity = await ValidateReview(request);
            if (validity != null) return validity;
            var result = await performanceReviewRepo.SavePerformanceReviewAsync(request.MapToEntity(request));
            if (result == Constants.ProcessStatus.Failed)
            {
                logger.LogError($"Failed to save performance review : {request.ReviewDate}");
                return responseHelper.SaveFailedResponse();
            }

            logger.LogInformation("Performance review saved successfully");
            return responseHelper.SaveSuccessResponse(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            return responseHelper.InternalServerErrorResponse();
        }
    }

    #endregion

    #region Private Methods

    private async Task<ResponseWithCode<dynamic>?> ValidateReview(PerformanceReviewRequestDto request)
    {
        if (request.StaffMemberId == request.ReviewerId)
        {
            logger.LogError(
                $"Trying to add Same Staff member{request.StaffMemberId} As Reviewer{request.ReviewerId}");
            return responseHelper.BadRequest(Constants.Messages.Error.SameStaffUsageForReviewer);
        }

        var staffMember =
            await staffMemberRepo.GetStaffMemberByIdAsync(request.StaffMemberId, Constants.Status.Active);
        var reviewer = await staffMemberRepo.GetStaffMemberByIdAsync(request.ReviewerId, Constants.Status.Active);

        if (request.ReviewDate.Kind != DateTimeKind.Utc)
        {
            request.ReviewDate = request.ReviewDate.ToUniversalTime();
        }

        if (staffMember == null)
        {
            logger.LogError($"Staff member with id {request.StaffMemberId} was not found");
            return responseHelper.BadRequest(Constants.Messages.Error.InvalidStaff);
        }

        if (reviewer != null) return null;
        logger.LogError($"Reviewer with id {request.ReviewerId} was not found");
        return responseHelper.BadRequest(Constants.Messages.Error.InvalidStaff, "for reviewer");
    }

    #endregion
}