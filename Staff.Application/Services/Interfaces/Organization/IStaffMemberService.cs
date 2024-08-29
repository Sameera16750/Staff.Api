using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;

namespace Staff.Application.Services.Interfaces.Organization;

public interface IStaffMemberService
{
    #region POST Methods

    Task<ResponseWithCode<dynamic>> SaveStaffMemberAsync(StaffMemberRequestDto staffMember);

    #endregion

    #region GET Methods

    Task<ResponseWithCode<dynamic>> GetStaffMemberByIdAsync(long id, int status);

    #endregion

    #region PUT Methods

    Task<ResponseWithCode<dynamic>> UpdateStaffMemberAsync(StaffMemberRequestDto staffMember,long id);

    #endregion
}