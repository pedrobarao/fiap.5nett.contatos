using System.Diagnostics.CodeAnalysis;
using Contatos.Cadastro.Api.Apis;
using Contatos.Cadastro.Api.Config;
using Contatos.ServiceDefaults;
using Contatos.ServiceDefaults.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();

builder.RegisterServices();

var withApiVersioning = builder.Services.AddApiVersioning();
builder.AddDefaultOpenApiConfig(withApiVersioning);
builder.AddHealthCheckConfig();

var app = builder.Build();

app.MapDefaultEndpoints();

var contatos = app.NewVersionedApi("Cadastro Contatos");
contatos.MapContatosApiV1();

app.UseDefaultOpenApiConfig();

app.UseHttpsRedirection();

app.Run();

namespace Contatos.Cadastro.Api
{
    [ExcludeFromCodeCoverage]
    public class CadastrosProgram
    {
    }
}