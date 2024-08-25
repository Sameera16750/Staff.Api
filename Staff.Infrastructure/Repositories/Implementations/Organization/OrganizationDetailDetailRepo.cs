using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Staff.Core.Constants;
using Staff.Core.Entities.Organization;
using Staff.Infrastructure.DBContext;
using Staff.Infrastructure.Models;
using Staff.Infrastructure.Repositories.Interfaces.Organization;

namespace Staff.Infrastructure.Repositories.Implementations.Organization;

public class OrganizationDetailDetailRepo(ApplicationDbContext context, ILogger<IOrganizationDetailRepo> logger)
    : IOrganizationDetailRepo
{
    #region POST Methods

    public async Task<long> SaveOrganizationAsync(OrganizationDetails organizationDetails)
    {
        logger.LogInformation("Saving company");
        context.Add(organizationDetails);
        var result = await context.SaveChangesAsync();
        if (result < 1)
        {
            logger.LogError("Company saving failed");
            return 0;
        }

        logger.LogInformation("Organization saving Success");
        return organizationDetails.Id;
    }

    #endregion

    #region GET Methods

    public async Task<OrganizationDetails?> GetDetailsAsync(long id, int status)
    {
        logger.LogInformation("Getting details");
        var result = await context.Organization.FirstOrDefaultAsync(o => (o.Id == id) && (o.Status == status));
        if (result == null)
        {
            logger.LogWarning("Organization not found");
            return null;
        }

        return result;
    }

    public async Task<PaginatedListDto<OrganizationDetails>?> GetAllOrganizationsAsync(string search, int pageNumber,
        int pageSize, int status)
    {
        var totalCount = await context.Organization.CountAsync();

        var result = await context.Organization
            .Where(o =>
                ((o.Name.Contains(search) || o.Address.Contains(search) || o.Email.Contains(search) ||
                  o.ContactNo.Contains(search)) && (o.Status == status)))
            .OrderBy(o => o.Id).Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var response = PaginatedListDto<OrganizationDetails>.Create(source: result,
            pageNumber: pageNumber, pageSize: pageSize, totalItems: totalCount);
        if (result.Count < 1)
        {
            logger.LogWarning("Organization not found");
            return null;
        }

        return response;
    }

    #endregion

    #region PUT Methods

    public async Task<long> UpdateOrganizationAsync(OrganizationDetails organizationDetails)
    {
        logger.LogInformation("Checking available organization");
        var existing = await context.Organization.FirstOrDefaultAsync(o =>
            (o.Id == organizationDetails.Id && o.Status == Constants.Status.Active));
        if (existing == null)
        {
            logger.LogError("Organization doesn't exist");
            return 0;
        }

        logger.LogInformation("Updating Organization");
        context.Entry(existing).CurrentValues.SetValues(organizationDetails);
        context.Entry(existing).State = EntityState.Modified;
        var result = await context.SaveChangesAsync();
        if (result < 1)
        {
            logger.LogError("Organization updating failed");
            return 0;
        }

        logger.LogInformation("Organization updating Success");
        return organizationDetails.Id;
    }

    #endregion
}