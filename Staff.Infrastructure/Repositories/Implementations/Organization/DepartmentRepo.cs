using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Staff.Core.Constants;
using Staff.Core.Entities.Organization;
using Staff.Infrastructure.DBContext;
using Staff.Infrastructure.Repositories.Interfaces.Organization;

namespace Staff.Infrastructure.Repositories.Implementations.Organization;

public class DepartmentRepo(ApplicationDbContext context, ILogger<IDepartmentRepo> logger)
    : IDepartmentRepo
{
    public async Task<long> SaveDepartment(Department department)
    {
        logger.LogInformation("Saving department ...");
        context.Department.Add(department);
        var result = await context.SaveChangesAsync();
        if (result < 1)
        {
            logger.LogInformation("Department saving failed.");
            return Constants.ProcessStatus.Failed;
        }

        return department.Id;
    }

    public async Task<Department?> GetDepartment(long id, int status)
    {
        logger.LogInformation("Getting department ...");
        var result = await context.Department.Include(d => d.OrganizationDetails)
            .FirstOrDefaultAsync(d => (d.Id == id) && (d.Status == status));
        if (result == null)
        {
            logger.LogWarning("Department not found.");
        }

        return result;
    }

    public async Task<long> UpdateDepartment(Department department)
    {
        logger.LogInformation("Checking available department ...");
        var existing =
            await context.Department.FirstOrDefaultAsync(d =>
                (d.Id == department.Id) && (d.Status == Constants.Status.Active));
        if (existing == null)
        {
            logger.LogWarning("Department not found.");
            return Constants.ProcessStatus.NotFound;
        }

        logger.LogInformation("Updating department ...");
        context.Entry(existing).CurrentValues.SetValues(department);
        context.Entry(existing).State = EntityState.Modified;
        var result = await context.SaveChangesAsync();
        if (result < 1)
        {
            logger.LogWarning("Department update failed.");
            return Constants.ProcessStatus.Failed;
        }

        return department.Id;
    }
}