using Staff.Core.Entities.Organization;
using Staff.Infrastructure.Models;

namespace Staff.Infrastructure.Repositories.Interfaces.Organization;

public interface IOrganizationDetailRepo
{
    #region POST Methods

    Task<long> SaveOrganization(OrganizationDetails organizationDetails);

    #endregion

    #region GET Methods

    Task<OrganizationDetails?> GetDetails(long id, int status);

    Task<PaginatedListDto<OrganizationDetails>?> GetAllOrganizations(string search, int pageNumber, int pageSize,
        int status);

    #endregion

    #region PUT Methods

    Task<long> UpdateOrganization(OrganizationDetails organizationDetails);

    #endregion
}