using Staff.Core.Constants;

namespace Staff.Application.Models.Request.common;

public class StatusDto
{
    public int Organization { get; set; } = Constants.Status.Active;
    public int Department { get; set; } = Constants.Status.Active;
    public int Designation { get; set; } = Constants.Status.Active;
    public int Staff { get; set; } = Constants.Status.Active;
}