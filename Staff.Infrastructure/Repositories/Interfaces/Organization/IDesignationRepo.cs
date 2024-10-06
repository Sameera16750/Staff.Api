using Staff.Core.Entities.Organization;
using Staff.Infrastructure.Models;
using Staff.Infrastructure.Models.Common;
using Staff.Infrastructure.Models.Staff;

namespace Staff.Infrastructure.Repositories.Interfaces.Organization;

public interface IDesignationRepo
{
    #region POST Methids

    Task<long> SaveDesignationAsync(Designation designation);

    #endregion

    #region GET Methods

    Task<Designation?> GetDesignationByNameAsync(string name, long department,long organization, int status);
    Task<Designation?> GetDesignationByIdAsync(long id,long organizationId, int status);
    Task<PaginatedListDto<Designation>?> GetAllDesignationsAsync(DesignationFiltersDto filters, StatusDto status,long organizationId);

    #endregion

    #region PUT Methods
    Task<long>UpdateDesignationAsync(Designation designation);
    #endregion

    #region DELETE Methods

    Task<long> DeleteDesignationAsync(long id,long organizationId);

    #endregion
}