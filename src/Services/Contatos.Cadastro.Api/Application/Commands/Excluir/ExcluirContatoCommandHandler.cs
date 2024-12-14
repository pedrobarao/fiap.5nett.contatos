using Commons.Domain.Communication;
using Commons.Domain.Messages;
using Commons.Domain.Messages.IntegrationEvents;
using Contatos.Cadastro.Api.Domain.Repositories;
using MediatR;
using MessageBus;

namespace Contatos.Cadastro.Api.Application.Commands.Excluir;

public class ExcluirContatoCommandHandler(IMessageBus bus, IContatoRepository repository)
    : CommandHandler, IRequestHandler<ExcluirContatoCommand, Result>
{
    public async Task<Result> Handle(ExcluirContatoCommand request, CancellationToken cancellationToken)
    {
        var contato = await repository.ObterContatoPorId(request.Id);

        if (contato is null) return Result.Success();

        repository.Excluir(contato);
        await repository.UnitOfWork.Commit();

        await bus.Publish(new ContatoExcluidoIntegrationEvent
        {
            AggregateId = contato.Id,
            Nome = contato.Nome.PrimeiroNome,
            Sobrenome = contato.Nome.Sobrenome,
            Telefones = contato.Telefones.Select(t => new ContatoExcluidoIntegrationEvent.Telefone
            {
                Ddd = t.Ddd,
                Numero = t.Numero,
                Tipo = t.Tipo.ToString()
            }).ToList(),
            Email = contato.Email?.Endereco
        }, cancellationToken);

        return Result.Success();
    }
}