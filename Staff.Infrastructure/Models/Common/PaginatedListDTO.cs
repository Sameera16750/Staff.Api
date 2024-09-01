namespace Staff.Infrastructure.Models;

public class PaginatedListDto<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public List<T> Items { get; set; }

    public PaginatedListDto(int pageNumber, int pageSize, int totalItems, List<T> items)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Items = items;
        TotalItems = totalItems;
        TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
    }

    public static PaginatedListDto<T> Create(List<T> source, int pageNumber, int pageSize, int totalItems)
    {
        return new PaginatedListDto<T>(pageNumber, pageSize, totalItems, source);
    }
}