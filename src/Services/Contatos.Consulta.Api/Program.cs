using Contatos.Consulta.Api.Apis;
using Contatos.Consulta.Api.Config;
using Contatos.ServiceDefaults;
using Contatos.ServiceDefaults.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();

builder.RegisterServices();

var withApiVersioning = builder.Services.AddApiVersioning();
builder.AddDefaultOpenApiConfig(withApiVersioning);

var app = builder.Build();

app.MapDefaultEndpoints();

var contatos = app.NewVersionedApi("Consulta Contatos");
contatos.MapConsultaContatosApiV1();

app.UseDefaultOpenApiConfig();

app.UseHttpsRedirection();

app.Run();