using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Staff.Core.Constants;
using Staff.Core.Entities.Organization;
using Staff.Infrastructure.DBContext;
using Staff.Infrastructure.Models;
using Staff.Infrastructure.Models.Common;
using Staff.Infrastructure.Models.Staff;
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

    public async Task<Designation?> GetDesignationByNameAsync(string name, long department, long organization,
        int status)
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

    public async Task<Designation?> GetDesignationByIdAsync(long id, long organizationId, int status)
    {
        logger.LogInformation($"Getting designation by id={id}");
        var designation = await context.Designation.Include(d => d.Department)
            .FirstOrDefaultAsync(d =>
                (d.Id == id && d.Status == status && d.Department!.OrganizationId == organizationId));
        if (designation == null)
        {
            logger.LogWarning($"Designation {id} not found");
        }

        return designation;
    }

    public async Task<PaginatedListDto<Designation>?> GetAllDesignationsAsync(DesignationFiltersDto filters,
        StatusDto status, long organizationId)
    {
        logger.LogInformation("Getting all designations ...");
        var query = context.Designation.Where(d =>
            ((d.Name.Contains(filters.Search) || d.Department!.Name.Contains(filters.Search) ||
              d.Department!.OrganizationDetails!.Name.Contains(filters.Search)) &&
             (filters.DepartmentId == 0 || d.DepartmentId == filters.DepartmentId) &&
             d.Status == status.Designation &&
             d.Department!.Status == status.Department &&
             d.Department!.OrganizationDetails!.Status == status.Organization &&
             d.Department.OrganizationId == organizationId
            ));
        var totalCount = await query.CountAsync();
        var designation = await query.Include(d => d.Department).OrderBy(d => d.Id)
            .Skip((filters.PageNumber - 1) * filters.PageSize).Take(filters.PageSize).ToListAsync();
        var response = PaginatedListDto<Designation>.Create(source: designation, pageNumber: filters.PageNumber,
            pageSize: filters.PageSize, totalItems: totalCount);
        if (totalCount < 1)
        {
            logger.LogWarning("No designations found");
        }

        return response;
    }

    #endregion

    #region PUT Methods

    public async Task<long> UpdateDesignationAsync(Designation designation)
    {
        logger.LogInformation("Checking available designations");
        var existing =
            await context.Designation.FirstOrDefaultAsync(d =>
                (d.Id == designation.Id && d.Status == designation.Status));
        if (existing == null)
        {
            logger.LogWarning($"Designation {designation.Id} not found");
            return Constants.ProcessStatus.NotFound;
        }

        logger.LogInformation("Updating designation ...");
        context.Entry(existing).CurrentValues.SetValues(designation);
        context.Entry(existing).State = EntityState.Modified;
        var result = await context.SaveChangesAsync();
        if (result < 1)
        {
            logger.LogWarning("Designation update failed");
            return Constants.ProcessStatus.Failed;
        }

        return designation.Id;
    }

    #endregion

    #region DELETE Methods

    public async Task<long> DeleteDesignationAsync(long id, long organizationId)
    {
        logger.LogInformation("Checking available designations");
        var existing =
            await context.Designation.FirstOrDefaultAsync(d =>
                (d.Id == id && d.Status != Constants.Status.Deleted && d.Department!.OrganizationId == organizationId));
        if (existing == null)
        {
            logger.LogWarning($"Designation {id} not found");
            return Constants.ProcessStatus.NotFound;
        }

        logger.LogInformation("Deleting designation ...");
        existing.Status = Constants.Status.Deleted;
        context.Designation.Update(existing);
        var result = await context.SaveChangesAsync();
        if (result >= 1) return existing.Id;
        logger.LogWarning("Department deletion failed.");
        return Constants.ProcessStatus.Failed;
    }

    #endregion
}