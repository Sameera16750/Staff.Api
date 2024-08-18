using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Staff.Core.Entities.Organization;
using Staff.Infrastructure.DBContext;
using Staff.Infrastructure.Repositories.Interfaces.Organization;

namespace Staff.Infrastructure.Repositories.Implementations.Organization;

public class OrganizationDetailDetailRepo(ApplicationDbContext context, ILogger<IOrganizationDetailRepo> logger)
    : IOrganizationDetailRepo
{
    public async Task<long> SaveCompany(OrganizationDetails organizationDetails)
    {
        logger.LogInformation("Saving company");
        context.Add(organizationDetails);
        var result = await context.SaveChangesAsync();
        if (result < 1)
        {
            logger.LogError("Company saving failed");
            return 0;
        }

        logger.LogInformation("Company saving Success");
        return organizationDetails.Id;
    }

    public async Task<OrganizationDetails?> GetDetails(long id)
    {
        logger.LogInformation("Getting details");
        var result = await context.Organization.FirstOrDefaultAsync(o => o.Id == id);
        if (result == null)
        {
            logger.LogWarning("Organization not found");
            return null;
        }

        return result;
    }
}