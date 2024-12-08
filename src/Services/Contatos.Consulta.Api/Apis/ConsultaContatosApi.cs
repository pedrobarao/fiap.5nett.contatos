using Commons.Domain.Communication;
using Contatos.Consulta.Api.Application.DTOs;
using Contatos.ServiceDefaults.Inputs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Contatos.Consulta.Api.Apis;

public static class ConsultaContatosApi
{
    public static RouteGroupBuilder MapConsultaContatosApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/contatos").HasApiVersion(1.0);

        api.MapPost("/{id}", ObterContato);
        api.MapPost("/", ListarContatos);

        return api;
    }

    private static async Task<Results<Ok<PagedResult<ObterContatoOutput>>, ValidationProblem>> ListarContatos(
        [AsParameters] ListarContatosServices services, PagedResultInput input)
    {
        var result = await services.Query.ExecuteAsync(input.PageSize, input.PageIndex, input.Query);
        return TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<ObterContatoOutput>, NotFound>> ObterContato(
        [AsParameters] ObterContatoServices services,
        [FromRoute] Guid id)
    {
        var result = await services.Query.ExecuteAsync(id);
        return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }
}