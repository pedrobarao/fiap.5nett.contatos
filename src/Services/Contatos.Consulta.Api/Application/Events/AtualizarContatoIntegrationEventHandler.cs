using Commons.Domain.Messages.IntegrationEvents;
using Contatos.Consulta.Api.Application.Commands;
using MassTransit;
using MassTransit.Mediator;

namespace Contatos.Consulta.Api.Application.Events;

public class AtualizarContatoIntegrationEventHandler(IMediator mediator) : IConsumer<ContatoAtualizadoIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ContatoAtualizadoIntegrationEvent> context)
    {
        var message = context.Message;
        var command = new AtualizarContatoCommand
        {
            Id = message.AggregateId,
            Nome = message.Nome,
            Email = message.Email,
            Telefones = message.Telefones.Select(t => new AtualizarContatoCommand.Telefone
            {
                Ddd = t.Ddd,
                Numero = t.Numero,
                Tipo = t.Tipo
            }).ToList()
        };

        await mediator.Send(command);
    }
}