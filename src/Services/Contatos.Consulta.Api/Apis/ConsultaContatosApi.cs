using Commons.Domain.Communication;
using Contatos.Consulta.Api.Domain.Entities;
using Contatos.Consulta.Api.Domain.Repositories;
using Contatos.ServiceDefaults.Inputs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Contatos.Consulta.Api.Apis;

public static class ConsultaContatosApi
{
    public static RouteGroupBuilder MapConsultaContatosApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/contatos").HasApiVersion(1.0);

        api.MapGet("/{id}", ObterContato);
        api.MapGet("/", ListarContatos);

        return api;
    }

    private static async Task<Results<Ok<PagedResult<Contato>>, ValidationProblem>> ListarContatos(
        [FromServices] IContatoRepository repository, [AsParameters] PagedResultInput input)
    {
        var contatos = await repository.ObterContatosPaginados(input.PageSize, input.PageIndex, input.Query);
        return TypedResults.Ok(contatos);
    }

    private static async Task<Results<Ok<Contato>, NotFound>> ObterContato(
        [FromServices] IContatoRepository repository,
        [FromRoute] Guid id)
    {
        var contato = await repository.ObterContatoPorId(id);
        return contato is not null ? TypedResults.Ok(contato) : TypedResults.NotFound();
    }
}