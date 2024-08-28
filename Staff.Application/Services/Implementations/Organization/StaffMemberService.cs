using Microsoft.Extensions.Logging;
using Staff.Application.Helpers.ResponseHelper;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Application.Services.Interfaces.Organization;
using Staff.Core.Constants;
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

    public async Task<ResponseWithCode<dynamic>> SaveStaffMemberAsync(StaffMemberRequestDto staffMember)
    {
        try
        {
            logger.LogInformation("Saving staff member ...");
            var designation =
                await designationRepo.GetDesignationByIdAsync(staffMember.DesignationId, Constants.Status.Active);
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
}