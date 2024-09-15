using System.ComponentModel.DataAnnotations;

namespace Staff.Application.Models.Request.Attendance;

public class UpdateAttendanceRequestDto
{
    [Required(ErrorMessage = "Check-In Time is required")]
    public required DateTime CheckInTime { get; set; }

    [Required(ErrorMessage = "Check-Out Time is required")]
    public required DateTime CheckOutTime { get; set; }
}