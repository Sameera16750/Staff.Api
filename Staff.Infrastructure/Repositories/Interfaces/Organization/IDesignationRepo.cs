using Staff.Core.Entities.Organization;

namespace Staff.Infrastructure.Repositories.Interfaces.Organization;

public interface IDesignationRepo
{
    #region POST Methids

    Task<long> SaveDesignationAsync(Designation designation);

    #endregion

    #region GET Methods

    Task<Designation?> GetDesignationByNameAsync(string name, long department, int status);

    #endregion
}