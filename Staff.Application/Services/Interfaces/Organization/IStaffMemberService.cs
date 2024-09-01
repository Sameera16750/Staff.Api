using Staff.Application.Models.Request.common;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Infrastructure.Models.Staff;

namespace Staff.Application.Services.Interfaces.Organization;

public interface IStaffMemberService
{
    #region POST Methods

    Task<ResponseWithCode<dynamic>> SaveStaffMemberAsync(StaffMemberRequestDto staffMember);

    #endregion

    #region GET Methods

    Task<ResponseWithCode<dynamic>> GetStaffMemberByIdAsync(long id, int status);
    
    Task<ResponseWithCode<dynamic>> GetAllStaffMembersAsync(StaffFiltersDto requestDto,StatusDto status);

    #endregion

    #region PUT Methods

    Task<ResponseWithCode<dynamic>> UpdateStaffMemberAsync(StaffMemberRequestDto staffMember,long id);

    #endregion
    
    #region DELETE Methods

    Task<ResponseWithCode<dynamic>> DeleteStaffMemberAsync(long id);

    #endregion
}