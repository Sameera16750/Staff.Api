namespace Staff.Application.Models.Request.Organization;

public class OrganizationListRequestDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SearchTerm { get; set; } = string.Empty;
}