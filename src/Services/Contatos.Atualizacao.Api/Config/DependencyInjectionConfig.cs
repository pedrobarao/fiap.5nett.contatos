using Contatos.Atualizacao.Api.Application.UseCases;
using Contatos.Atualizacao.Api.Domain;
using Contatos.Atualizacao.Api.Infra.Data;
using Contatos.Atualizacao.Api.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Contatos.Atualizacao.Api.Config;

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
        services.AddScoped<IAtualizarContatoUseCase, AtualizarContatoUseCase>();
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