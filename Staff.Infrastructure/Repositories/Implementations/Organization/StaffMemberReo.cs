using Microsoft.Extensions.Logging;
using Staff.Core.Constants;
using Staff.Core.Entities.Organization;
using Staff.Infrastructure.DBContext;
using Staff.Infrastructure.Repositories.Interfaces.Organization;

namespace Staff.Infrastructure.Repositories.Implementations.Organization;

public class StaffMemberReo(ApplicationDbContext context, ILogger<IStaffMemberRepo> logger)
    : IStaffMemberRepo
{
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
}