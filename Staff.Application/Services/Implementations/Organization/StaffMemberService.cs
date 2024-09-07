using System.Net;
using Microsoft.Extensions.Logging;
using Staff.Application.Helpers.ResponseHelper;
using Staff.Application.Models.Request.common;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Application.Models.Response.Organization;
using Staff.Application.Services.Interfaces.Organization;
using Staff.Core.Constants;
using Staff.Infrastructure.Models.Staff;
using Staff.Infrastructure.Repositories.Interfaces.Organization;

namespace Staff.Application.Services.Implementations.Organization;

public class StaffMemberService(
    IStaffMemberRepo staffMemberRepo,
    IDesignationRepo designationRepo,
    ILogger<IStaffMemberService> logger,
    IResponseHelper responseHelper)
    : IStaffMemberService
{
    #region POST Methods

    public async Task<ResponseWithCode<dynamic>> SaveStaffMemberAsync(StaffMemberRequestDto staffMember,
        long organizationId)
    {
        try
        {
            logger.LogInformation("Saving staff member ...");
            var designation =
                await designationRepo.GetDesignationByIdAsync(staffMember.DesignationId, organizationId,
                    Constants.Status.Active);
            if (designation == null) return responseHelper.BadRequest(Constants.Messages.Error.InvalidDesignation);

            if (staffMember.Birthday.Kind != DateTimeKind.Utc)
            {
                staffMember.Birthday = staffMember.Birthday.ToUniversalTime();
            }

            var age = DateTime.Now.Year - staffMember.Birthday.Year;
            switch (age)
            {
                case < 18:
                    return responseHelper.BadRequest(Constants.Messages.Error.AgeMinimum, 18.ToString());
                case > 90:
                    return responseHelper.BadRequest(Constants.Messages.Error.AgeMaximum, 90.ToString());
                default:
                {
                    var result = await staffMemberRepo.SaveStaffMemberAsync(staffMember.MapToEntity(staffMember));
                    return result == Constants.ProcessStatus.Failed
                        ? responseHelper.SaveFailedResponse()
                        : responseHelper.SaveSuccessResponse(result);
                }
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

    public async Task<ResponseWithCode<dynamic>> GetStaffMemberByIdAsync(long id,long organizationId, int status)
    {
        try
        {
            logger.LogInformation("Getting staff member ...");
            var staffMember = await staffMemberRepo.GetStaffMemberByIdAsync(id, organizationId,status);
            if (staffMember == null) return responseHelper.NotFoundErrorResponse();
            return responseHelper.CreateResponseWithCode<dynamic>(HttpStatusCode.OK,
                new StaffMemberResponseDto().MapToResponse(staffMember));
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            return responseHelper.InternalServerErrorResponse();
        }
    }

    public async Task<ResponseWithCode<dynamic>> GetAllStaffMembersAsync(StaffFiltersDto filters,
        StatusDto status, long organizationId)
    {
        try
        {
            logger.LogInformation("Getting all staff members ...");
            var staff = await staffMemberRepo.GetAllMembersAsync(filters, status, organizationId);
            var response = new PaginatedListResponseDto<StaffMemberResponseDto>();
            if (staff != null)
            {
                response = response.ToPaginatedListResponse(staff,
                    new StaffMemberResponseDto().MapToListResponse(staff.Items));
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

    public async Task<ResponseWithCode<dynamic>> UpdateStaffMemberAsync(StaffMemberRequestDto staffMember, long id,
        long organizationId)
    {
        try
        {
            var validations = await ValidateRequestDataSync(staffMember, organizationId);
            if (validations != null) return validations;
            var updatedStaffMember = staffMember.MapToEntity(staffMember);
            updatedStaffMember.Id = id;
            var result = await staffMemberRepo.UpdateStaffMemberAsync(updatedStaffMember);
            return result == Constants.ProcessStatus.NotFound
                ? responseHelper.BadRequest(Constants.Messages.Error.InvalidStaff)
                : result == Constants.ProcessStatus.Failed
                    ? responseHelper.UpdateFailedResponse()
                    : responseHelper.UpdateSuccessResponse(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            return responseHelper.InternalServerErrorResponse();
        }
    }

    #endregion

    #region DELETE Methods

    public async Task<ResponseWithCode<dynamic>> DeleteStaffMemberAsync(long id)
    {
        try
        {
            var result = await staffMemberRepo.DeleteStaffMemberAsync(id);
            if (result == Constants.ProcessStatus.NotFound)
            {
                return responseHelper.BadRequest(Constants.Messages.Error.InvalidStaff);
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

    #region Private Methods

    private async Task<ResponseWithCode<dynamic>?> ValidateRequestDataSync(StaffMemberRequestDto staffMember,
        long organizationId)
    {
        var designation =
            await designationRepo.GetDesignationByIdAsync(staffMember.DesignationId, organizationId,
                Constants.Status.Active);
        if (designation == null)
        {
            logger.LogError("Invalid designation");
            return responseHelper.BadRequest(Constants.Messages.Error.InvalidDesignation);
        }

        if (staffMember.Birthday.Kind != DateTimeKind.Utc)
        {
            staffMember.Birthday = staffMember.Birthday.ToUniversalTime();
        }

        var age = DateTime.Now.Year - staffMember.Birthday.Year;

        switch (age)
        {
            case < 18:
            {
                logger.LogError("Invalid age range");
                return responseHelper.BadRequest(Constants.Messages.Error.AgeMinimum, 18.ToString());
            }
            case > 90:
            {
                logger.LogError("Invalid age range");
                return responseHelper.BadRequest(Constants.Messages.Error.AgeMaximum, 90.ToString());
            }
            default:
            {
                return null;
            }
        }
    }

    #endregion
}