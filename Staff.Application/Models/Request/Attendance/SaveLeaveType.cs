using System.ComponentModel.DataAnnotations;
using Staff.Core.Constants;
using Staff.Core.Entities.Attendance;

namespace Staff.Application.Models.Request.Attendance;

public class SaveLeaveType
{
    [Required(ErrorMessage = "Leave type is required")]
    public required string Type { get; set; }

    public LeaveType MapToEntity(SaveLeaveType leaveType, long organizationId)
    {
        return new LeaveType
        {
            Id = 0,
            Type = leaveType.Type,
            Status = Constants.Status.Active,
            OrganizationId = organizationId
        };
    }
}