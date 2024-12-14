using Contatos.Cadastro.Api.Domain.Repositories;
using Contatos.Cadastro.Api.Infra.Data;
using Contatos.Cadastro.Api.Infra.Data.Repositories;
using MessageBus;
using Microsoft.EntityFrameworkCore;

namespace Contatos.Cadastro.Api.Config;

public static class DependencyInjectionConfig
{
    public static IHostApplicationBuilder RegisterServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        RegisterApplicationServices(builder.Services);
        RegisterDomainServices(builder.Services);
        RegisterInfraServices(builder);

        return builder;
    }

    private static void RegisterApplicationServices(IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        // services.AddScoped<IRequestHandler<CadastrarContatoCommand, Result<Guid>>, CadastrarContatoCommandHandler>();
        // services.AddScoped<IRequestHandler<AtualizarContatoCommand, Result>, AtualizarContatoCommandHandler>();
        // services.AddScoped<IRequestHandler<ExcluirContatoCommand, Result>, ExcluirContatoCommandHandler>();
    }

    private static void RegisterDomainServices(IServiceCollection services)
    {
        services.AddScoped<IContatoRepository, ContatoRepository>();
    }

    private static void RegisterInfraServices(IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ContatoDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddMessageBus(builder.Configuration);
    }
}