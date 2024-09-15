using System.Runtime.InteropServices.JavaScript;
using Staff.Core.Entities.Attendance;

namespace Staff.Application.Models.Response.Attendance;

public class AttendanceResponseDto
{
    public long Id { get; set; }
    public long StaffId { get; set; }
    public string StaffName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }

    public AttendanceResponseDto MapToResponse(AttendanceDetails attendance)
    {
        return new AttendanceResponseDto
        {
            Id = attendance.Id,
            StaffId = attendance.StaffMemberId,
            StaffName = $"{attendance.StaffMember!.FirstName} {attendance.StaffMember!.LastName}",
            Date = attendance.Date,
            CheckInTime = attendance.CheckIn,
            CheckOutTime = attendance.CheckOut
        };
    }

    public List<AttendanceResponseDto> MapToListResponse(List<AttendanceDetails> attendance)
    {
        return attendance.Select(MapToResponse).ToList();
    }
}