using Microsoft.AspNetCore.Mvc;

namespace Contatos.ServiceDefaults.Inputs;

public class PagedResultInput
{
    [FromQuery] public int PageSize { get; set; } = 10;

    [FromQuery] public int PageIndex { get; set; } = 0;
    
    [FromQuery]
    public string? Query { get; set; }
}