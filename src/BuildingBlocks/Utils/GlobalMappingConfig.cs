using Commons.Domain.Communication;
using Mapster;

namespace Utils;

public static class GlobalMappingConfig
{
    public static void Register()
    {
        TypeAdapterConfig.GlobalSettings.ForType(typeof(PagedResult<>), typeof(PagedResult<>))
            .Map("TotalItems", "TotalItems")
            .Map("TotalPages", "TotalPages")
            .Map("PageIndex", "PageIndex")
            .Map("PageSize", "PageSize")
            .Map("Filter", "Filter")
            .Map("Items", "Items.Adapt<IEnumerable<T>>()");
    }
}