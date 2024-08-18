using Staff.Core.Entities.Organization;

namespace Staff.Infrastructure.Repositories.Interfaces.Organization;

public interface IOrganizationDetailRepo
{
    Task<long> SaveCompany(OrganizationDetails organizationDetails);
}