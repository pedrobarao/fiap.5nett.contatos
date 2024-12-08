using Commons.Domain.Communication;
using Contatos.Cadastro.Api.Application.Commands.Criar;
using MediatR;

namespace Contatos.Cadastro.Api.Config;

public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Services
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        
        // Application - Commands
        services.AddScoped<IRequestHandler<CadastrarContatoCommand, Result<Guid>>, CadastrarContatoCommandHandler>();
        
        return services;
    }
}