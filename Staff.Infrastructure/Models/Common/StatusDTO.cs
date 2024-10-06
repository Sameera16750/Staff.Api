using Staff.Core.Constants;

namespace Staff.Infrastructure.Models.Common;

public class StatusDto
{
    public int Organization { get; set; } = Constants.Status.Active;
    public int Department { get; set; } = Constants.Status.Active;
    public int Designation { get; set; } = Constants.Status.Active;
    public int Staff { get; set; } = Constants.Status.Active;
    public int PerformanceReview { get; set; } = Constants.Status.Active;
    public int Attendance { get; set; } = Constants.Status.Active;
    public int LeaveType { get; set; } = Constants.Status.Active;
}