using Staff.Core.Entities.Organization;
using Staff.Infrastructure.Models;

namespace Staff.Infrastructure.Repositories.Interfaces.Organization;

public interface IOrganizationDetailRepo
{
    #region POST Methods

    Task<long> SaveOrganizationAsync(OrganizationDetails organizationDetails);

    #endregion

    #region GET Methods

    Task<OrganizationDetails?> GetDetailsAsync(long id, int status);

    Task<PaginatedListDto<OrganizationDetails>?> GetAllOrganizationsAsync(string search, int pageNumber, int pageSize,
        int status);

    #endregion

    #region PUT Methods

    Task<long> UpdateOrganizationAsync(OrganizationDetails organizationDetails);

    #endregion
}