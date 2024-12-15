using Commons.Domain.Messages.IntegrationEvents;
using Contatos.Consulta.Api.Application.Commands;
using MassTransit;
using MediatR;

namespace Contatos.Consulta.Api.Application.Events;

public class ExcluirContatoIntegrationEventHandler(IMediator mediator) : IConsumer<ContatoExcluidoIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ContatoExcluidoIntegrationEvent> context)
    {
        var message = context.Message;
        var command = new ExcluirContatoCommand
        {
            Id = message.AggregateId
        };

        await mediator.Send(command);
    }
}