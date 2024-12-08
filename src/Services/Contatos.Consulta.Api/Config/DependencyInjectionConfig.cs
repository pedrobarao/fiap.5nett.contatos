using Contatos.Consulta.Api.Application.Events;
using Contatos.Consulta.Api.Application.Mappings;
using Contatos.Consulta.Api.Application.Queries;
using Contatos.Consulta.Api.Domain.Repositories;
using Contatos.Consulta.Api.Infra.Data;
using Contatos.Consulta.Api.Infra.Data.Repositories;
using Mapster;
using MessageBus;
using Microsoft.EntityFrameworkCore;
using Utils;

namespace Contatos.Consulta.Api.Config;

public static class DependencyInjectionConfig
{
    public static IHostApplicationBuilder RegisterServices(this IHostApplicationBuilder builder)
    {
        RegisterApplicationServices(builder.Services);
        RegisterDomainServices(builder.Services);
        RegisterInfraServices(builder.Services, builder.Configuration);

        builder.Services.AddMapster();
        GlobalMappingConfig.Register();
        MappingConfig.Register();

        builder.Services.AddMessageBus(x => { x.AddConsumer<CriarContatoIntegrationEventHandler>(); },
            builder.Configuration);

        return builder;
    }

    private static void RegisterApplicationServices(IServiceCollection services)
    {
        services.AddScoped<IListarContatoQuery, ListarContatoQuery>();
        services.AddScoped<IObterContatoQuery, ObterContatoQuery>();
    }

    private static void RegisterDomainServices(IServiceCollection services)
    {
        services.AddScoped<IContatoRepository, ContatoRepository>();
    }

    private static void RegisterInfraServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ContatoDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
    }
}