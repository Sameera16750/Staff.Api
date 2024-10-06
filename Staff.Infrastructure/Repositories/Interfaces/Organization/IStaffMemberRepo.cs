using Staff.Core.Entities.Organization;
using Staff.Infrastructure.Models;
using Staff.Infrastructure.Models.Common;
using Staff.Infrastructure.Models.Staff;

namespace Staff.Infrastructure.Repositories.Interfaces.Organization;

public interface IStaffMemberRepo
{
    #region POST Methods

    Task<long> SaveStaffMemberAsync(StaffMember staffMember);

    #endregion

    #region GET Methods

    Task<StaffMember?> GetStaffMemberByIdAsync(long id, long organizationId, int status);

    Task<PaginatedListDto<StaffMember>?> GetAllMembersAsync(StaffFiltersDto filters, StatusDto status,
        long organizationId);

    #endregion

    #region PUT Methods

    Task<long> UpdateStaffMemberAsync(StaffMember staffMember, long organizationId);

    #endregion

    #region DELETE Methods

    public Task<long> DeleteStaffMemberAsync(long id, long organizationId);

    #endregion
}