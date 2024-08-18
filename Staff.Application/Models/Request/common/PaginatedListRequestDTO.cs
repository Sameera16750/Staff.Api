using System.ComponentModel.DataAnnotations;

namespace Staff.Application.Models.Request.common;

public class PaginatedListRequestDto
{
    [Range(minimum:1, maximum:int. MaxValue,ErrorMessage = "Minimum page number is 1")]
    public int PageNumber { get; set; } = 1;
    [Range(minimum:1, maximum:int. MaxValue,ErrorMessage = "Minimum page size is 1")]
    public int PageSize { get; set; } = 10;
    public string SearchTerm { get; set; } = string.Empty;
}