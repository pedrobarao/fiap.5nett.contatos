namespace Commons.Domain.Communication;

public class PagedResult<T> where T : class
{
    public PagedResult(IEnumerable<T>? items, int totalItems, int pageIndex, int pageSize, string? filter)
    {
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        if (TotalPages < 0) TotalPages = 0;
        Items = items ?? [];
        TotalItems = totalItems;
        PageIndex = pageIndex;
        PageSize = pageSize;
        Filter = filter;
    }

    public IEnumerable<T> Items { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string? Filter { get; set; }
}