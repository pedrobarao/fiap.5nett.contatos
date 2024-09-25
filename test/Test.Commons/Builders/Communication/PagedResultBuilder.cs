using Commons.Domain.Communication;

namespace Test.Commons.Builders.Communication;

public static class PagedResultBuilder
{
    public static PagedResult<T> Build<T>(IEnumerable<T>? items,
        int pageIndex = 1,
        int pageSize = 10,
        string filter = "")
        where T : class
    {
        var itemsList = items?.ToList();
        return new PagedResult<T>(itemsList, itemsList?.Count ?? 0, pageIndex, pageSize, filter);
    }
}