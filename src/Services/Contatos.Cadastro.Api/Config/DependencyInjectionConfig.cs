using Contatos.Cadastro.Api.Application.UseCases;
using Contatos.Cadastro.Api.Domain;
using Contatos.Cadastro.Api.Infra.Data;
using Contatos.Cadastro.Api.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Contatos.Cadastro.Api.Config;

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
        services.AddScoped<ICadastrarContatoUseCase, CadastrarContatoUseCase>();
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