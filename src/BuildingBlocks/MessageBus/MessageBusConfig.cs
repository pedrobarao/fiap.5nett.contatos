using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessageBus;

public static class MessageBusConfig
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services,
        Action<IBusRegistrationConfigurator> configureConsumers, IConfiguration config)
    {
        var messageBusSettings = new MessageBusSettings();
        config.GetRequiredSection("MessageBus").Bind(messageBusSettings);

        services.AddMassTransit(
                x =>
                {
                    configureConsumers(x);

                    x.UsingRabbitMq(
                        (context, cfg) =>
                        {
                            cfg.Host(messageBusSettings.Host, "/", h =>
                            {
                                h.Username(messageBusSettings.Username);
                                h.Password(messageBusSettings.Password);
                            });
                        });
                })
            .BuildServiceProvider();
        
        services.AddSingleton<IMessageBus, MessageBus>();

        return services;
    }
}