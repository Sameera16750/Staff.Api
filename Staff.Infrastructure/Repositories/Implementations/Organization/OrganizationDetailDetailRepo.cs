using Microsoft.Extensions.Logging;
using Staff.Core.Entities.Organization;
using Staff.Infrastructure.DBContext;
using Staff.Infrastructure.Repositories.Interfaces.Organization;

namespace Staff.Infrastructure.Repositories.Implementations.Organization;

public class OrganizationDetailDetailRepo(ApplicationDbContext context, ILogger<IOrganizationDetailRepo> logger)
    : IOrganizationDetailRepo
{
    private readonly ApplicationDbContext _context = context;
    private readonly ILogger<IOrganizationDetailRepo> _logger = logger;

    public async Task<long> SaveCompany(OrganizationDetails organizationDetails)
    {
        _logger.LogInformation("Saving company");
        _context.Add(organizationDetails);
        var result = await _context.SaveChangesAsync();
        if (result < 1)
        {
            _logger.LogError("Company saving failed");
            return 0;
        }

        _logger.LogInformation("Company saving Success");
        return organizationDetails.Id;
    }
}