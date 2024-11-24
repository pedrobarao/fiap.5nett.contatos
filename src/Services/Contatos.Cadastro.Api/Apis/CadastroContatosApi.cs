using Contatos.Cadastro.Api.Application.DTOs.Inputs;
using Contatos.Cadastro.Api.Application.DTOs.Outputs;
using Contatos.ServiceDefaults.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Contatos.Cadastro.Api.Apis;

public static class CadastroContatosApi
{
    public static RouteGroupBuilder MapCadastroContatosApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/contatos").HasApiVersion(1.0);

        api.MapPost("/", CadastrarContato);

        return api;
    }

    private static async Task<Results<Created<ContatoCriadoOutput>, ValidationProblem>> CadastrarContato(
        HttpContext context,
        [AsParameters] CadastroContatosServices services,
        [FromBody] NovoContatoInput input)
    {
        var result = await services.useCase.ExecuteAsync(input);

        if (!result.IsSuccess)
        {
            return TypedResults.Extensions.InvalidOperation(result.Errors, context);
        }

        return TypedResults.Created($"https://consulta-contatos/{result.Data!.Id}", result.Data);
    }
}