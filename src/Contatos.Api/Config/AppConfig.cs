using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Contatos.Api.Extensions;

namespace Contatos.Api.Config;

[ExcludeFromCodeCoverage]
public static class AppConfig
{
    public static IServiceCollection AddAppConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddEndpointsApiExplorer();

        services.AddSwaggerConfig();

        services.RegisterServices(configuration);

        services.AddHealthCheck(configuration);

        return services;
    }

    public static WebApplication UseAppConfig(this WebApplication app)
    {
        app.UseSwaggerConfig();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        // Comente esta linha para desabilitar a aplicação de migrações
        app.ApplyMigrations();

        app.UseHealthCheck();

        return app;
    }
}