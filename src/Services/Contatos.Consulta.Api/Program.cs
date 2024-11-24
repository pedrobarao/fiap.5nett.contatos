using Contatos.Consulta.Api.Apis;
using Contatos.Consulta.Api.Config;
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

var contatos = app.NewVersionedApi("Atualização Contatos");
contatos.MapConsultaContatosApiV1();

app.UseDefaultOpenApiConfig();

app.UseHttpsRedirection();

app.Run();