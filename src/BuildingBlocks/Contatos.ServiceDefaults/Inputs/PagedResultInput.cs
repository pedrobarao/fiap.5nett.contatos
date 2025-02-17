using Microsoft.AspNetCore.Mvc;

namespace Contatos.ServiceDefaults.Inputs;

public class PagedResultInput
{
    [FromQuery(Name = "pageSize")] public int PageSize { get; set; } = 10;

    [FromQuery(Name = "pageIndex")] public int PageIndex { get; set; } = 0;

    [FromQuery(Name = "query")] public string? Query { get; set; }
}