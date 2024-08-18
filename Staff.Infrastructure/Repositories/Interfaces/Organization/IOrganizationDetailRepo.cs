using Staff.Core.Entities.Organization;
using Staff.Infrastructure.Models;

namespace Staff.Infrastructure.Repositories.Interfaces.Organization;

public interface IOrganizationDetailRepo
{
    Task<long> SaveCompany(OrganizationDetails organizationDetails);
    Task<OrganizationDetails?> GetDetails(long id);
    
    Task<PaginatedListDto<OrganizationDetails>?> GetAllOrganizations(string search,int pageNumber, int pageSize);
}