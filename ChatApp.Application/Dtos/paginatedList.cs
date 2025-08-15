namespace ChatApp.Application.Dtos;

public class PaginatedList<T>
{
    public List<T> Items { get; }
    public long PageNumber { get; }
    public long TotalPages { get; }
    public long TotalCount { get; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    public PaginatedList(List<T> items, long count, long pageNumber, long pageSize)
    {
        TotalCount = count;
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        Items = items;
    }
}