using System.Diagnostics.CodeAnalysis;
using Contatos.Application.UseCases;
using Contatos.Application.UseCases.Interfaces;
using Contatos.Domain.Repositories;
using Contatos.Infra.Data;
using Contatos.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Contatos.Api.Config;

[ExcludeFromCodeCoverage]
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
        services.AddScoped<IObterContatoUseCase, ObterContatoUseCase>();
        services.AddScoped<IListarContatoUseCase, ListarContatoUseCase>();
        services.AddScoped<IExcluirContatoUseCase, ExcluirContatoUseCase>();
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