using Contatos.Consulta.Api.Application.UseCases;
using Contatos.Consulta.Api.Domain;
using Contatos.Consulta.Api.Infra.Data;
using Contatos.Consulta.Api.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Contatos.Consulta.Api.Config;

public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterApplicationServices(services);
        RegisterDomainServices(services);
        RegisterInfraServices(services, configuration);

        return services;
    }

    private static void RegisterApplicationServices(IServiceCollection services)
    {
        services.AddScoped<IListarContatoUseCase, ListarContatoUseCase>();
        services.AddScoped<IObterContatoUseCase, ObterContatoUseCase>();
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