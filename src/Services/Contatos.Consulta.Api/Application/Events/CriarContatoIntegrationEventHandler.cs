using Commons.Domain.Messages.IntegrationEvents;
using Contatos.Consulta.Api.Application.Commands;
using MassTransit;
using MediatR;

namespace Contatos.Consulta.Api.Application.Events;

public class CriarContatoIntegrationEventHandler(IMediator mediator) : IConsumer<ContatoCriadoIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ContatoCriadoIntegrationEvent> context)
    {
        var message = context.Message;
        var command = new CriarContatoCommand
        {
            Id = message.AggregateId,
            Nome = message.Nome,
            Sobrenome = message.Sobrenome,
            Email = message.Email,
            Telefones = message.Telefones.Select(t => new CriarContatoCommand.Telefone
            {
                Ddd = t.Ddd,
                Numero = t.Numero,
                Tipo = t.Tipo
            }).ToList()
        };

        await mediator.Send(command);
    }
}