using Staff.Infrastructure.Models;

namespace Staff.Application.Models.Response.Common;

public class PaginatedListResponseDto<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public List<T> Items { get; set; }

    public PaginatedListResponseDto<T1> ToPaginatedListResponse<T1, T2>(PaginatedListDto<T2> paginatedListDto, List<T1> items)
    {
        return new PaginatedListResponseDto<T1>
        {
            PageNumber = paginatedListDto.PageNumber,
            PageSize = paginatedListDto.PageSize,
            Items = items,
            TotalItems = paginatedListDto.TotalItems,
            TotalPages = paginatedListDto.TotalPages,
        };
    }
}