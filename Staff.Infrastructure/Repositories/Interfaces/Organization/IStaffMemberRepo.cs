using Staff.Core.Entities.Organization;

namespace Staff.Infrastructure.Repositories.Interfaces.Organization;

public interface IStaffMemberRepo
{
    #region POST Methods

    Task<long> SaveStaffMemberAsync(StaffMember staffMember);

    #endregion

    #region GET Methods

    Task<StaffMember?> GetStaffMemberByIdAsync(long id, int status);

    #endregion

    #region PUT Methods

    Task<long> UpdateStaffMemberAsync(StaffMember staffMember);

    #endregion
}