using System.ComponentModel.DataAnnotations;
using Staff.Core.Constants;
using Staff.Core.Entities.Attendance;

namespace Staff.Application.Models.Request.Attendance;

public class SaveAttendanceRequestDto
{
    [Required(ErrorMessage = "Staff Id is required")]
    public required long StaffMemberId { get; set; }
    
    [Required(ErrorMessage = "Date is required")]
    public required DateTime DateAndTime { get; set; }

    public AttendanceDetails MapToEntity(SaveAttendanceRequestDto requestDto)
    {
        return new AttendanceDetails
        {
            Id = 0,
            Date = requestDto.DateAndTime,
            StaffMemberId = requestDto.StaffMemberId,
            Status = Constants.Status.Active,
            CheckIn = requestDto.DateAndTime,
            CheckOut = null,
        };
    }
}