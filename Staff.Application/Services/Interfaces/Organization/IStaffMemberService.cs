using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;

namespace Staff.Application.Services.Interfaces.Organization;

public interface IStaffMemberService
{
    #region POST Methods

    Task<ResponseWithCode<dynamic>> SaveStaffMemberAsync(StaffMemberRequestDto staffMember);

    #endregion
}