using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Staff.Core.Constants;
using Staff.Core.Entities.Organization;
using Staff.Infrastructure.DBContext;
using Staff.Infrastructure.Repositories.Interfaces.Organization;

namespace Staff.Infrastructure.Repositories.Implementations.Organization;

public class DesignationRepo(ApplicationDbContext context, ILogger<IDesignationRepo> logger) : IDesignationRepo
{
    #region POST Methods

    public async Task<long> SaveDesignationAsync(Designation designation)
    {
        logger.LogInformation("Saving designation..");
        context.Designation.Add(designation);
        var result = await context.SaveChangesAsync();
        if (result < 1)
        {
            logger.LogError("Failed to save designation.");
            return Constants.ProcessStatus.Failed;
        }

        return designation.Id;
    }

    #endregion

    #region GET Methods

    public async Task<Designation?> GetDesignationByNameAsync(string name, long department, int status)
    {
        logger.LogInformation("Checking available designations");
        var existing = await context.Designation.FirstOrDefaultAsync(d =>
            (d.DepartmentId == department && d.Name.Equals(name) &&
             d.Status == status));
        if (existing != null)
        {
            logger.LogWarning($"Designation {name} already exists");
        }

        return existing;
    }

    #endregion
}