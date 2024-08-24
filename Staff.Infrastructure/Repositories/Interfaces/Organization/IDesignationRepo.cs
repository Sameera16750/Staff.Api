using Staff.Core.Entities.Organization;

namespace Staff.Infrastructure.Repositories.Interfaces.Organization;

public interface IDesignationRepo
{
    #region POST Methids

    Task<long> SaveDesignation(Designation designation);

    #endregion
}