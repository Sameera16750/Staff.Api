using System.Net;
using Microsoft.Extensions.Logging;
using Staff.Application.Helpers.ResponseHelper;
using Staff.Application.Models.Request.common;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Application.Models.Response.Organization;
using Staff.Application.Services.Interfaces.Organization;
using Staff.Core.Constants;
using Staff.Infrastructure.Models.Common;
using Staff.Infrastructure.Models.Staff;
using Staff.Infrastructure.Repositories.Interfaces.Organization;

namespace Staff.Application.Services.Implementations.Organization;

public class PerformanceReviewService(
    IPerformanceReviewRepo performanceReviewRepo,
    IStaffMemberRepo staffMemberRepo,
    IResponseHelper responseHelper,
    ILogger<IPerformanceReviewService> logger) : IPerformanceReviewService
{
    #region POST Methods

    public async Task<ResponseWithCode<dynamic>> SavePerformanceReview(PerformanceReviewRequestDto request,
        long organizationId)
    {
        try
        {
            logger.LogInformation("Saving performance review ...");
            var validity = await ValidateReview(request, organizationId);
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

    #region GET Methods

    public async Task<ResponseWithCode<dynamic>> GetPerformanceReviewByIdAsync(long id, long organizationId,
        StatusDto status)
    {
        try
        {
            logger.LogInformation("Getting performance review ...");
            var review = await performanceReviewRepo.GetPerformanceReviewByIdAsync(id, organizationId, status);
            if (review == null)
            {
                logger.LogError($"Failed to get performance review : {id}");
                return responseHelper.NotFoundErrorResponse();
            }

            logger.LogInformation($"Performance review retrieved successfully by using {id}");
            return responseHelper.CreateResponseWithCode<dynamic>(HttpStatusCode.OK,
                new PerformanceReviewResponseDto().MapToResponse(review));
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            return responseHelper.InternalServerErrorResponse();
        }
    }

    public async Task<ResponseWithCode<dynamic>> GetAllPerformanceReviewsAsync(PerformanceReviewFilterDto filters,
        long organizationId, StatusDto status)
    {
        try
        {
            logger.LogInformation("GetAll reviews processing ...");
            var reviews = await performanceReviewRepo.GetAllPerformanceReviewsAsync(filters, organizationId, status);
            var response = new PaginatedListResponseDto<PerformanceReviewResponseDto>();
            if (reviews != null)
            {
                response = response.ToPaginatedListResponse(
                    reviews,
                    new PerformanceReviewResponseDto().MapToListResponse(reviews.Items)
                );
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

    public async Task<ResponseWithCode<dynamic>> UpdatePerformanceReviewAsync(PerformanceReviewRequestDto request,
        long id, long organizationId)
    {
        try
        {
            logger.LogInformation("Updating performance review ...");
            var validity = await ValidateReview(request, organizationId);
            if (validity != null) return validity;
            var review = request.MapToEntity(request);
            review.Id = id;
            var result =
                await performanceReviewRepo.UpdatePerformanceReviewAsync(review, organizationId);
            if (result == Constants.ProcessStatus.Failed)
            {
                logger.LogError($"Failed to update performance review : {request.ReviewDate}");
                return responseHelper.UpdateFailedResponse();
            }

            if (result == Constants.ProcessStatus.NotFound)
            {
                logger.LogError($"Failed to update performance review : {request.ReviewDate}");
                return responseHelper.BadRequest(Constants.Messages.Error.InvalidReview);
            }

            logger.LogInformation("Performance review saved successfully");
            return responseHelper.UpdateSuccessResponse(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            return responseHelper.InternalServerErrorResponse();
        }
    }

    #endregion

    #region DELETE Methods

    public async Task<ResponseWithCode<dynamic>> DeletePerformanceReviewAsync(long id, long organizationId)
    {
        try
        {
            logger.LogInformation("Delete review processing ...");
            var result = await performanceReviewRepo.DeletePerformanceReviewAsync(id, organizationId);
            return result == Constants.ProcessStatus.Failed
                ? responseHelper.DeleteFailedErrorResponse()
                : result == Constants.ProcessStatus.NotFound
                    ? responseHelper.BadRequest(Constants.Messages.Error.InvalidReview)
                    : responseHelper.DeleteSuccessResponse(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            return responseHelper.InternalServerErrorResponse();
        }
    }

    #endregion

    #region Private Methods

    private async Task<ResponseWithCode<dynamic>?> ValidateReview(PerformanceReviewRequestDto request,
        long organizationId)
    {
        if (request.StaffMemberId == request.ReviewerId)
        {
            logger.LogError(
                $"Trying to add Same Staff member{request.StaffMemberId} As Reviewer{request.ReviewerId}");
            return responseHelper.BadRequest(Constants.Messages.Error.SameStaffUsageForReviewer);
        }

        var staffMember =
            await staffMemberRepo.GetStaffMemberByIdAsync(request.StaffMemberId, organizationId,
                Constants.Status.Active);
        var reviewer =
            await staffMemberRepo.GetStaffMemberByIdAsync(request.ReviewerId, organizationId, Constants.Status.Active);

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