using Staff.Core.Entities.Organization;

namespace Staff.Infrastructure.Repositories.Interfaces.Organization;

public interface IStaffMemberRepo
{
    #region POST Methods

    Task<long>SaveStaffMemberAsync(StaffMember staffMember);

    #endregion
}