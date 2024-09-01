using System.ComponentModel.DataAnnotations;

namespace Staff.Infrastructure.Models.Common;

public class PaginationDto
{
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Minimum page number is 1")]
    public int PageNumber { get; set; } = 1;

    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Minimum page size is 1")]
    public int PageSize { get; set; } = 10;

    public string Search { get; set; } = string.Empty;
    
    public long OrganizationId { get; set; } = 0;
    
}