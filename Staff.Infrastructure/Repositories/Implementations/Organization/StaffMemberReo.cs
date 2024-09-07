using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Staff.Application.Models.Request.common;
using Staff.Core.Constants;
using Staff.Core.Entities.Organization;
using Staff.Infrastructure.DBContext;
using Staff.Infrastructure.Models;
using Staff.Infrastructure.Models.Staff;
using Staff.Infrastructure.Repositories.Interfaces.Organization;

namespace Staff.Infrastructure.Repositories.Implementations.Organization;

public class StaffMemberReo(ApplicationDbContext context, ILogger<IStaffMemberRepo> logger)
    : IStaffMemberRepo
{
    #region POST Methods

    public async Task<long> SaveStaffMemberAsync(StaffMember staffMember)
    {
        logger.LogInformation("Saving staff member");
        context.StaffMember.Add(staffMember);
        var result = await context.SaveChangesAsync();
        if (result < 1)
        {
            logger.LogError("Failed to save staff member");
            return Constants.ProcessStatus.Failed;
        }

        logger.LogInformation("Staff member saved");
        return staffMember.Id;
    }

    #endregion

    #region GET Methods

    public async Task<StaffMember?> GetStaffMemberByIdAsync(long id, int status)
    {
        logger.LogInformation("Getting staff member by id");
        var staffMembers = await context.StaffMember.Include(s => s.Designation)
            .Include(s => s.Designation!.Department)
            .FirstOrDefaultAsync(s =>
                (s.Id == id && s.Status == status && s.Designation!.Status == Constants.Status.Active &&
                 s.Designation.Department!.Status == Constants.Status.Active &&
                 s.Designation.Department.OrganizationDetails!.Status == Constants.Status.Active));

        if (staffMembers == null) logger.LogWarning("Staff member not found");
        return staffMembers;
    }

    public async Task<PaginatedListDto<StaffMember>?> GetAllMembersAsync(StaffFiltersDto filters, StatusDto status,long organizationId)
    {
        logger.LogInformation("Getting all staff members ...");

        var query = context.StaffMember.Where(s =>
            ((s.FirstName.Contains(filters.Search) ||
              (string.IsNullOrWhiteSpace(s.LastName) && s.LastName!.Contains(filters.Search)) ||
              (s.Address.Contains(filters.Search) || (s.ContactNumber.Contains(filters.Search) ||
                                                      (string.IsNullOrWhiteSpace(s.Email) &&
                                                       s.Email!.Contains(filters.Search)) ||
                                                      (s.Designation!.Name.Contains(filters.Search))))) &&
             (s.Status == status.Staff)) && (s.Status == status.Staff && s.Designation!.Status == status.Designation &&
                                             s.Designation.Department!.Status == status.Department &&
                                             s.Designation.Department.OrganizationDetails!.Status ==
                                             status.Organization) &&
            (filters.DesignationId <= 0 || s.DesignationId == filters.DesignationId) &&
            (filters.DepartmentId <= 0 || s.Designation.DepartmentId == filters.DepartmentId) &&
            (organizationId <= 0 || s.Designation.Department.OrganizationId == organizationId));

        var count = await query.CountAsync();

        var staff = await query.Include(s => s.Designation)
            .OrderBy(d => d.Id)
            .Skip((filters.PageNumber - 1) * filters.PageSize).Take(filters.PageSize).ToListAsync();
        if (count < 1)
        {
            logger.LogWarning("No staff members found");
        }

        return PaginatedListDto<StaffMember>.Create(source: staff, pageNumber: filters.PageNumber,
            pageSize: filters.PageSize, totalItems: count);
    }

    #endregion

    #region PUT Methods

    public async Task<long> UpdateStaffMemberAsync(StaffMember staffMember)
    {
        logger.LogInformation("checking available staff member..");
        var exist = await context.StaffMember.FirstOrDefaultAsync(s =>
            (s.Id == staffMember.Id && staffMember.Status == Constants.Status.Active));

        if (exist == null)
        {
            logger.LogWarning("Staff member not found");
            return Constants.ProcessStatus.NotFound;
        }

        logger.LogInformation("Updating staff member");
        context.Entry(exist).CurrentValues.SetValues(staffMember);
        context.Entry(exist).State = EntityState.Modified;
        var result = await context.SaveChangesAsync();

        if (result < 1)
        {
            logger.LogWarning("Failed to update staff member");
            return Constants.ProcessStatus.Failed;
        }

        logger.LogInformation("Staff member updated");
        return staffMember.Id;
    }

    #endregion

    #region DELETE Methods

    public async Task<long> DeleteStaffMemberAsync(long id)
    {
        logger.LogInformation("Checking available designations");
        var existing =
            await context.StaffMember.FirstOrDefaultAsync(s => (s.Id == id && s.Status != Constants.Status.Deleted));
        if (existing == null)
        {
            logger.LogWarning($"Staff member {id} not found");
            return Constants.ProcessStatus.NotFound;
        }

        logger.LogInformation("Deleting staff member ...");
        existing.Status = Constants.Status.Deleted;
        context.StaffMember.Update(existing);
        var result = await context.SaveChangesAsync();
        if (result >= 1) return existing.Id;
        logger.LogWarning("Staff member deletion failed.");
        return Constants.ProcessStatus.Failed;
    }

    #endregion
}