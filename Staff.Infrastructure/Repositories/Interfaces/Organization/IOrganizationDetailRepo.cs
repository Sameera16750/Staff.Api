using Staff.Core.Entities.Organization;
using Staff.Infrastructure.Models;

namespace Staff.Infrastructure.Repositories.Interfaces.Organization;

public interface IOrganizationDetailRepo
{
    Task<long> SaveOrganization(OrganizationDetails organizationDetails);
    Task<long> UpdateOrganization(OrganizationDetails organizationDetails);
    Task<OrganizationDetails?> GetDetails(long id,int status);
    Task<PaginatedListDto<OrganizationDetails>?> GetAllOrganizations(string search,int pageNumber, int pageSize,int status);
}