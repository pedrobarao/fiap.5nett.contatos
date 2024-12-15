using Commons.Domain.Messages.IntegrationEvents;
using Contatos.Cadastro.Api.Domain.Repositories;
using Contatos.Cadastro.Api.Infra.Data;
using Contatos.Cadastro.Api.Infra.Data.Repositories;
using MassTransit;
using MessageBus;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

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

        var messageBusSettings = new MessageBusSettings();
        builder.Configuration.GetRequiredSection("MessageBus").Bind(messageBusSettings);

        builder.Services.AddMassTransit(
                busConfig =>
                {
                    busConfig.UsingRabbitMq(
                        (context, cfg) =>
                        {
                            cfg.Host(new Uri(messageBusSettings.Host), "/", h =>
                            {
                                h.Username(messageBusSettings.Username);
                                h.Password(messageBusSettings.Password);
                            });

                            cfg.Message<ContatoCriadoIntegrationEvent>(configTopology =>
                            {
                                configTopology.SetEntityName("ContatoCriadoExchange");
                            });
                            
                            cfg.Publish<ContatoCriadoIntegrationEvent>(publishConfig =>
                            {
                                publishConfig.ExchangeType = ExchangeType.Topic;
                            });
                        });
                })
            .BuildServiceProvider();

        builder.Services.AddSingleton<IMessageBus, MessageBus.MessageBus>();
    }
}