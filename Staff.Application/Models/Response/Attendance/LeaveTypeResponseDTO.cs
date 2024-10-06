using Staff.Core.Entities.Attendance;

namespace Staff.Application.Models.Response.Attendance;

public class LeaveTypeResponseDto
{
    public long Id { get; set; }
    public string Name { get; set; }

    public LeaveTypeResponseDto MapToResponse(LeaveType leaveType)
    {
        return new LeaveTypeResponseDto
        {
            Id = leaveType.Id,
            Name = leaveType.Type
        };
    }

    public List<LeaveTypeResponseDto> MapToResponseList(List<LeaveType> leaveTypes)
    {
        return leaveTypes.Select(MapToResponse).ToList();
    }
}