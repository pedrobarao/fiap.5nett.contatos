using System.Data;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Contatos.Consulta.Api.Config;

public static class HealthCheckConfig
{
    public static IHostApplicationBuilder AddHealthCheckConfig(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddMongoDb(
                builder.Configuration.GetConnectionString("DefaultConnection") ??
                throw new NoNullAllowedException(),
                failureStatus: HealthStatus.Unhealthy,
                tags: ["ready"]);

        return builder;
    }
}