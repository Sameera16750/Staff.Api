using Staff.Application.Models.Request.common;
using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;
using Staff.Infrastructure.Models.Common;
using Staff.Infrastructure.Models.Staff;

namespace Staff.Application.Services.Interfaces.Organization;

public interface IStaffMemberService
{
    #region POST Methods

    Task<ResponseWithCode<dynamic>> SaveStaffMemberAsync(StaffMemberRequestDto staffMember, long organizationId);

    #endregion

    #region GET Methods

    Task<ResponseWithCode<dynamic>> GetStaffMemberByIdAsync(long id, long organizationId,int status);

    Task<ResponseWithCode<dynamic>> GetAllStaffMembersAsync(StaffFiltersDto requestDto, StatusDto status,long organizationId);

    #endregion

    #region PUT Methods

    Task<ResponseWithCode<dynamic>> UpdateStaffMemberAsync(StaffMemberRequestDto staffMember, long id,
        long organizationId);

    #endregion

    #region DELETE Methods

    Task<ResponseWithCode<dynamic>> DeleteStaffMemberAsync(long id,long organizationId);

    #endregion
}