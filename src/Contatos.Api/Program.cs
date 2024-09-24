using System.Diagnostics.CodeAnalysis;
using Contatos.Api.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppConfig(builder.Configuration);

var app = builder.Build();

app.UseAppConfig();

app.Run();


[ExcludeFromCodeCoverage]
public static partial class Program
{
}