using Contatos.Cadastro.Api.Application.Commands.Atualizar;
using Contatos.Cadastro.Api.Application.Commands.Criar;
using Contatos.Cadastro.Api.Application.Commands.Excluir;
using Contatos.ServiceDefaults.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Contatos.Cadastro.Api.Apis;

public static class ContatosApi
{
    public static RouteGroupBuilder MapContatosApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/contatos").HasApiVersion(1.0);

        api.MapPost("/", CadastrarContato);
        api.MapPut("/", AtualizarContato);
        api.MapDelete("/", ExcluirContato);

        return api;
    }

    private static async Task<Results<Created<Guid>, ValidationProblem>> CadastrarContato(
        HttpContext context,
        IMediator mediator,
        [FromBody] CadastrarContatoCommand command)
    {
        var result = await mediator.Send(command);

        if (!result.IsSuccess) return TypedResults.Extensions.InvalidOperation(result.Errors, context);

        return TypedResults.Created($"https://consulta-contatos/{result.Value}", result.Value);
    }

    private static async Task<Results<NoContent, ValidationProblem>> AtualizarContato(
        HttpContext context,
        IMediator mediator,
        [FromBody] AtualizarContatoCommand command)
    {
        var result = await mediator.Send(command);

        if (!result.IsSuccess) return TypedResults.Extensions.InvalidOperation(result.Errors, context);

        return TypedResults.NoContent();
    }

    private static async Task<NoContent> ExcluirContato(
        IMediator mediator, 
        [FromRoute] Guid id)
    {
        await mediator.Send(new ExcluirContatoCommand { Id = id });
        return TypedResults.NoContent();
    }
}