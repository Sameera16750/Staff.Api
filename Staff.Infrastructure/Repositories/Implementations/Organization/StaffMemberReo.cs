using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Staff.Core.Constants;
using Staff.Core.Entities.Organization;
using Staff.Infrastructure.DBContext;
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
        var staffMembers = await context.StaffMember.Include(s => s.Designation).FirstOrDefaultAsync(s =>
            (s.Id == id && s.Status == status && s.Designation!.Status == Constants.Status.Active &&
             s.Designation.Department!.Status == Constants.Status.Active &&
             s.Designation.Department.OrganizationDetails!.Status == Constants.Status.Active));

        if (staffMembers == null) logger.LogWarning("Staff member not found");
        return staffMembers;
    }

    #endregion

    #region PUT Methods

    public async Task<long> UpdateStaffMemberAsync(StaffMember staffMember)
    {
        logger.LogInformation("checking available staff member..");
        var exist = await context.StaffMember.FirstOrDefaultAsync(s =>
            (s.Id == staffMember.Id && staffMember.Status == Constants.Status.Active));
        
        if (exist==null)
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
}