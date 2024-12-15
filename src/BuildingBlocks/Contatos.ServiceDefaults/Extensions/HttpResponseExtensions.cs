using Commons.Domain.Communication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Contatos.ServiceDefaults.Extensions;

public static class HttpResponseExtensions
{
    public static ValidationProblem InvalidOperation(this IResultExtensions _,
        List<Error>? errors,
        HttpContext context)
    {
        var errorsDictionary =
            errors?.ToDictionary<Error?, string, string[]>(_ => "details", error => [error.ToString()]);

        return TypedResults.ValidationProblem(type: "https://datatracker.ietf.org/doc/html/rfc7807",
            title: "Erro ao processar a requisição.",
            instance: context.Request.Path,
            detail: "Um ou mais erros ocorreram ao processar a requisição.",
            errors: errorsDictionary ?? [],
            // Adicionar somente para fins de depuração ou se for um sistema interno a organização
            extensions: new Dictionary<string, object?>
            {
                { "traceId", context.TraceIdentifier }
            });
    }
}