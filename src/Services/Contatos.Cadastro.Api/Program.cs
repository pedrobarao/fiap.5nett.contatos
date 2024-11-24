using Contatos.Cadastro.Api.Apis;
using Contatos.Cadastro.Api.Config;
using Contatos.ServiceDefaults;
using Contatos.ServiceDefaults.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();

builder.Services.RegisterServices(builder.Configuration);

var withApiVersioning = builder.Services.AddApiVersioning();
builder.AddDefaultOpenApiConfig(withApiVersioning);

var app = builder.Build();

app.MapDefaultEndpoints();

var contatos = app.NewVersionedApi("Cadastro Contatos");
contatos.MapCadastroContatosApiV1();

app.UseDefaultOpenApiConfig();

app.UseHttpsRedirection();

app.Run();