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
}