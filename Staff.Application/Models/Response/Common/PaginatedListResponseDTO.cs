namespace Staff.Application.Models.Response.Common;

public class PaginatedListResponseDto<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public List<T> Items { get; set; }

    public PaginatedListResponseDto(int pageNumber, int pageSize, int totalItems, List<T> items)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Items = items;
        TotalItems = totalItems;
        TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
    }

    public static PaginatedListResponseDto<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        return new PaginatedListResponseDto<T>(pageNumber, pageSize, count, items);
    }
}