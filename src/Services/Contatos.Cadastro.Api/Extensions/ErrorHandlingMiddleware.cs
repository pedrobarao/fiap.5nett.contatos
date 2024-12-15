using System.Net;

namespace Contatos.Cadastro.Api.Extensions;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception)
        {
            HandleRequestExceptionAsync(context, HttpStatusCode.InternalServerError);
            throw;
        }
    }
    
    private static void HandleRequestExceptionAsync(HttpContext context, HttpStatusCode statusCode)
    {
        context.Response.StatusCode = (int)statusCode;
    }
}