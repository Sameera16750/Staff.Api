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
    #region POST Methods

    public async Task<long> SaveDepartmentAsync(Department department)
    {
        logger.LogInformation("Saving department ...");
        context.Department.Add(department);
        var result = await context.SaveChangesAsync();
        if (result >= 1) return department.Id;
        logger.LogError("Department saving failed.");
        return Constants.ProcessStatus.Failed;
    }

    #endregion

    #region GET Methods

    public async Task<Department?> GetDepartmentByNameAsync(string name, long organization, int status)
    {
        logger.LogInformation("Checking available departments");
        var existing = await context.Department.FirstOrDefaultAsync(d =>
            (d.OrganizationId == organization && d.Name.Equals(name) &&
             d.Status == status));
        if (existing == null)
        {
            logger.LogWarning($"Designation {name} not exists");
        }

        return existing;
    }

    public async Task<Department?> GetDepartmentAsync(long id, long organizationId, int status)
    {
        logger.LogInformation("Getting department ...");
        var result = await context.Department.Include(d => d.OrganizationDetails)
            .FirstOrDefaultAsync(d =>
                (organizationId == 0 || d.OrganizationId == organizationId) && (d.Id == id) && (d.Status == status) &&
                (d.OrganizationDetails!.Status == Constants.Status.Active));
        if (result == null)
        {
            logger.LogWarning("Department not found.");
        }

        return result;
    }

    public async Task<PaginatedListDto<Department>?> GetAllDepartmentsAsync(string search, int pageNumber, int pageSize,
        int departmentStatus, long organization, int organizationStatus)
    {
        logger.LogInformation("Getting all departments ...");
        var totalCount = await context.Department.Where(o =>
                (((o.Name.Contains(search) || (o.OrganizationDetails!.Name.Contains(search))) &&
                  (o.Status == departmentStatus) &&
                  (o.OrganizationDetails!.Id == organization && o.OrganizationDetails!.Status == organizationStatus))))
            .CountAsync();

        var result = await context.Department.Include(d => d.OrganizationDetails).Where(o =>
                (((o.Name.Contains(search) || (o.OrganizationDetails!.Name.Contains(search))) &&
                  (o.Status == departmentStatus) && (o.OrganizationDetails!.Id == organization &&
                                                     o.OrganizationDetails!.Status == organizationStatus))))
            .OrderBy(d => d.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        var response = PaginatedListDto<Department>.Create(source: result, pageNumber: pageNumber,
            pageSize: pageSize, totalItems: totalCount);

        if (result.Count >= 1) return response;
        logger.LogWarning("No departments found.");
        return null;
    }

    #endregion

    #region PUT Methods

    public async Task<long> UpdateDepartmentAsync(Department department)
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

    #endregion

    #region DELETE Methods

    public async Task<long> DeleteDepartmentAsync(long id, long organizationId)
    {
        logger.LogInformation("Checking available department ...");
        var existing =
            await context.Department.FirstOrDefaultAsync(d =>
                (d.OrganizationId == organizationId) && (d.Id == id) && (d.Status != Constants.Status.Deleted));
        if (existing == null)
        {
            logger.LogWarning("Department not found.");
            return Constants.ProcessStatus.NotFound;
        }

        logger.LogInformation("Updating department ...");
        var deleted = existing;
        deleted.Status = Constants.Status.Deleted;

        context.Entry(existing).CurrentValues.SetValues(deleted);
        context.Entry(existing).State = EntityState.Modified;
        var result = await context.SaveChangesAsync();
        if (result < 1)
        {
            logger.LogWarning("Department update failed.");
            return Constants.ProcessStatus.Failed;
        }

        return existing.Id;
    }

    #endregion
}