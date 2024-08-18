using Microsoft.Extensions.Logging;
using Staff.Core.Entities.Company;
using Staff.Infrastructure.DBContext;
using Staff.Infrastructure.Repositories.Interfaces.company;

namespace Staff.Infrastructure.Repositories.Implementations.company;

public class CompanyDetailDetailRepo(ApplicationDbContext context, ILogger<ICompanyDetailRepo> logger)
    : ICompanyDetailRepo
{
    private readonly ApplicationDbContext _context = context;
    private readonly ILogger<ICompanyDetailRepo> _logger = logger;

    public async Task<long> SaveCompany(CompanyDetails companyDetails)
    {
        _logger.LogInformation("Saving company");
        _context.Add(companyDetails);
        var result = await _context.SaveChangesAsync();
        if (result < 1)
        {
            _logger.LogError("Company saving failed");
            return 0;
        }

        _logger.LogInformation("Company saving Success");
        return companyDetails.Id;
    }
}