using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Contatos.ServiceDefaults.Extensions;

public static class HttpResponseExtensions
{
    public static ValidationProblem InvalidOperation(this IResultExtensions _,
        List<string> errors,
        HttpContext context)
    {
        return TypedResults.ValidationProblem(type: "https://datatracker.ietf.org/doc/html/rfc7807",
            title: "Erro ao processar a requisição.",
            instance: context.Request.Path,
            detail: "Um ou mais erros ocorreram ao processar a requisição.",
            errors: new Dictionary<string, string[]> { { "details", errors.ToArray() } },
            // Adicionar somente para fins de depuração ou se for um sistema interno a organização
            extensions: new Dictionary<string, object?>
            {
                { "traceId", context.TraceIdentifier }
            });
    }
}