using System.Data;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Contatos.Cadastro.Api.Config;

public static class HealthCheckConfig
{
    public static IHostApplicationBuilder AddHealthCheckConfig(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new NoNullAllowedException(),
                failureStatus: HealthStatus.Unhealthy,
                tags: ["ready"]);

        return builder;
    }
}