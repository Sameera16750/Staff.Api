using Staff.Core.Entities.Organization;
using Staff.Infrastructure.Models;

namespace Staff.Infrastructure.Repositories.Interfaces.Organization;

public interface IDesignationRepo
{
    #region POST Methids

    Task<long> SaveDesignationAsync(Designation designation);

    #endregion

    #region GET Methods

    Task<Designation?> GetDesignationByNameAsync(string name, long department, int status);
    Task<Designation?> GetDesignationByIdAsync(long id, int status);
    Task<PaginatedListDto<Designation>?> GetAllDesignationsAsync(string search, int pageNumber, int pageSize,
        int designationStatus, long department, int departmentStatus, int organizationStatus);

    #endregion
}