using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Contatos.Api.Config;

[ExcludeFromCodeCoverage]
public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API de Contatos", Version = "v1" });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        return services;
    }

    public static WebApplication UseSwaggerConfig(this WebApplication app)
    {
        // Swagger
        app.UseSwaggerUI();
        app.UseSwagger();

        // Scalar /scalar/v1
        // app.UseSwagger(options => { options.RouteTemplate = "openapi/{documentName}.json"; });
        // app.MapScalarApiReference(o => { o.Theme = "light"; });

        // Redoc
        app.UseReDoc(c => { c.RoutePrefix = "doc"; });

        return app;
    }
}