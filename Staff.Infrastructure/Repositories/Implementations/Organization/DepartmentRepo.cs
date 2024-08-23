using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Staff.Core.Constants;
using Staff.Core.Entities.Organization;
using Staff.Infrastructure.DBContext;
using Staff.Infrastructure.Models;
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

    public async Task<PaginatedListDto<Department>?> GetAllDepartments(string search, int pageNumber, int pageSize,
        int departmentStatus, long organization, int organizationStatus)
    {
        logger.LogInformation("Getting all departments ...");
        var totalCount = await context.Department.Where((d =>
                d.OrganizationDetails!.Id == organization && d.OrganizationDetails.Status == organizationStatus))
            .CountAsync();

        var result = await context.Department.Include(d => d.OrganizationDetails).Where(o =>
                (((o.Name.Contains(search) || (o.OrganizationDetails!.Name.Contains(search))) &&
                  (o.Status == departmentStatus) && (o.OrganizationDetails!.Id == organization &&
                                                     o.OrganizationDetails!.Status == organizationStatus))))
            .OrderBy(d => d.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        var response = PaginatedListDto<Department>.Create(source: result.AsQueryable(), pageNumber: pageNumber,
            pageSize: pageSize, totalItems: totalCount);

        if (result.Count >= 1) return response;
        logger.LogWarning("No departments found.");
        return null;
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