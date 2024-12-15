using Commons.Domain.Communication;
using Commons.Domain.Messages;
using Commons.Domain.Messages.IntegrationEvents;
using Contatos.Cadastro.Api.Domain.Entities;
using Contatos.Cadastro.Api.Domain.Repositories;
using Contatos.Cadastro.Api.Domain.ValueObjects;
using MediatR;
using MessageBus;

namespace Contatos.Cadastro.Api.Application.Commands.Criar;

public class CriarContatoCommandHandler(IMessageBus bus, IContatoRepository repository)
    : CommandHandler, IRequestHandler<CriarContatoCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CriarContatoCommand request,
        CancellationToken cancellationToken)
    {
        var nome = new Nome(request.Nome, request.Sobrenome);
        var email = new Email(request.Email!);
        var telefones = request.Telefones.Select(t => new Telefone(t.Ddd, t.Numero, t.Tipo)).ToList();
        var contato = new Contato(nome, telefones, email);

        ValidationResult = contato.Validar();

        if (ValidationResult.IsInvalid) return Result.Failure<Guid>(ValidationResult.Errors);

        repository.Adicionar(contato);

        if ((await PersistData(repository.UnitOfWork)).IsValid)
            await bus.Publish(new ContatoCriadoIntegrationEvent
            {
                AggregateId = contato.Id,
                Nome = contato.Nome.PrimeiroNome,
                Sobrenome = contato.Nome.Sobrenome,
                Telefones = contato.Telefones.Select(t => new ContatoCriadoIntegrationEvent.Telefone
                {
                    Ddd = t.Ddd,
                    Numero = t.Numero,
                    Tipo = t.Tipo.ToString()
                }).ToList(),
                Email = contato.Email?.Endereco
            }, cancellationToken);

        return ValidationResult.IsInvalid ? Result.Failure<Guid>(ValidationResult.Errors) : Result.Success(contato.Id);
    }
}