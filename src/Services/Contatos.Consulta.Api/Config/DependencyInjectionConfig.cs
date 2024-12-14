using Contatos.Consulta.Api.Application.Events;
using Contatos.Consulta.Api.Domain.Repositories;
using Contatos.Consulta.Api.Infra;
using Mapster;
using MessageBus;
using MongoDB.Driver;
using Utils;

namespace Contatos.Consulta.Api.Config;

public static class DependencyInjectionConfig
{
    public static IHostApplicationBuilder RegisterServices(this IHostApplicationBuilder builder)
    {
        RegisterApplicationServices(builder.Services);
        RegisterDomainServices(builder.Services);
        RegisterInfraServices(builder);

        return builder;
    }

    private static void RegisterApplicationServices(IServiceCollection services)
    {
        services.AddMapster();
        GlobalMappingConfig.Register();
    }

    private static void RegisterDomainServices(IServiceCollection services)
    {
        services.AddScoped<IContatoRepository, ContatoRepository>();
    }

    private static void RegisterInfraServices(IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton(sp =>
        {
            var dbName = builder.Configuration["ConnectionStrings:DatabaseName"];
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(dbName);
        });

        builder.Services.AddMessageBus(
            builder.Configuration,
            x => { x.AddConsumer<CriarContatoIntegrationEventHandler>(); });
    }
}