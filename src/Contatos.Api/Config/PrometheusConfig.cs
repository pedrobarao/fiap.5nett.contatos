using Prometheus;

namespace Contatos.Api.Config;

public static class PrometheusConfig
{
    public static WebApplication AddPrometheusConfig(this WebApplication app)
    {
        app.UseMetricServer("/metrics");
        app.UseHttpMetrics();
        return app;
    }
}