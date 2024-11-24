using Contatos.Atualizacao.Api.Application.DTOs;
using Contatos.ServiceDefaults.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Contatos.Atualizacao.Api.Apis;

public static class AtualizacaoContatosApi
{
    public static RouteGroupBuilder MapAtualizacaoContatosApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/contatos").HasApiVersion(1.0);

        api.MapPost("/", AtualizarContato);

        return api;
    }

    private static async Task<Results<NoContent, ValidationProblem>> AtualizarContato(
        HttpContext context,
        [AsParameters] AtualizacaoContatosServices services,
        [FromBody] AtualizarContatoInput input)
    {
        var result = await services.useCase.ExecuteAsync(input);

        if (!result.IsSuccess)
        {
            return TypedResults.Extensions.InvalidOperation(result.Errors, context);
        }

        return TypedResults.NoContent();
    }
}