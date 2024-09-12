using Staff.Core.Entities.Attendance;

namespace Staff.Application.Models.Response.Attendance;

public class AttendanceResponseDto
{
    public long Id { get; set; }
    public long StaffId { get; set; }
    public string StaffName { get; set; } = string.Empty;
    public string Date { get; set; }=string.Empty;
    public string? CheckInTime { get; set; } = string.Empty;
    public string? CheckOutTime { get; set; } = string.Empty;

    public AttendanceResponseDto MapToResponse(AttendanceDetails attendance)
    {
        return new AttendanceResponseDto
        {
            Id = attendance.Id,
            StaffId = attendance.StaffMemberId,
            StaffName = $"{attendance.StaffMember!.FirstName} {attendance.StaffMember!.LastName}",
            Date = attendance.Date.ToLongDateString(),
            CheckInTime = attendance.CheckIn?.ToLongTimeString(),
            CheckOutTime = attendance.CheckOut?.ToLongTimeString()
        };
    }

    public List<AttendanceResponseDto> MapToListResponse(List<AttendanceDetails> attendance)
    {
        return attendance.Select(MapToResponse).ToList();
    }
}