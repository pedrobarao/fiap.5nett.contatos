using Contatos.Consulta.Api.Application.Events;
using Contatos.Consulta.Api.Domain.Entities;
using Contatos.Consulta.Api.Domain.Repositories;
using Contatos.Consulta.Api.Domain.ValueObjects;
using Contatos.Consulta.Api.Infra;
using MassTransit;
using MessageBus;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using RabbitMQ.Client;

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
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        services.AddScoped<CriarContatoIntegrationEventHandler>();
        services.AddScoped<AtualizarContatoIntegrationEventHandler>();
        services.AddScoped<ExcluirContatoIntegrationEventHandler>();
    }

    private static void RegisterDomainServices(IServiceCollection services)
    {
        services.AddScoped<IContatoRepository, ContatoRepository>();
    }

    private static void RegisterInfraServices(IHostApplicationBuilder builder)
    {
        BsonClassMap.RegisterClassMap<Contato>(cm =>
        {
            cm.AutoMap();
            cm.SetIgnoreExtraElements(true);
            cm.MapMember(c => c.Id)
                .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
        });

        BsonClassMap.RegisterClassMap<Nome>(cm =>
        {
            cm.AutoMap();
            cm.SetIgnoreExtraElements(true);
        });

        BsonClassMap.RegisterClassMap<Telefone>(cm =>
        {
            cm.AutoMap();
            cm.SetIgnoreExtraElements(true);
        });

        BsonClassMap.RegisterClassMap<Email>(cm =>
        {
            cm.AutoMap();
            cm.SetIgnoreExtraElements(true);
        });

        builder.Services.AddSingleton<IMongoClient>(services =>
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("MongoDB connection string is not configured.");
            }

            return new MongoClient(connectionString);
        });

        builder.Services.AddScoped<IMongoDatabase>(services =>
        {
            var dbName = builder.Configuration["ConnectionStrings:DatabaseName"];
            var client = services.GetRequiredService<IMongoClient>();

            if (string.IsNullOrEmpty(dbName))
            {
                throw new InvalidOperationException("MongoDB database name is not configured.");
            }

            return client.GetDatabase(dbName);
        });

        builder.Services.AddScoped<IContatoRepository, ContatoRepository>();

        var messageBusSettings = new MessageBusSettings();
        builder.Configuration.GetRequiredSection("MessageBus").Bind(messageBusSettings);

        builder.Services.AddMassTransit(
                busConfig =>
                {
                    busConfig.AddConsumer<CriarContatoIntegrationEventHandler>();

                    busConfig.UsingRabbitMq(
                        (context, cfg) =>
                        {
                            cfg.Host(new Uri(messageBusSettings.Host), "/", h =>
                            {
                                h.Username(messageBusSettings.Username);
                                h.Password(messageBusSettings.Password);
                            });

                            cfg.ReceiveEndpoint("ContatoCriadoQueue", re =>
                            {
                                re.ConfigureConsumeTopology = false;
                                re.Bind("ContatoCriadoExchange",
                                    binding => { binding.ExchangeType = ExchangeType.Topic; });
                                re.ConfigureConsumer<CriarContatoIntegrationEventHandler>(context);
                                // re.Durable = true;
                                // re.AutoDelete = false;
                                re.UseMessageRetry(r =>
                                {
                                    r.Immediate(5);
                                    r.Interval(999999, TimeSpan.FromMinutes(1));
                                });
                            });
                        });
                })
            .BuildServiceProvider();

        builder.Services.AddSingleton<IMessageBus, MessageBus.MessageBus>();
        builder.Services.AddMediator();
    }
}