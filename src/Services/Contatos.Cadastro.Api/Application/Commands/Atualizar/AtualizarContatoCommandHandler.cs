using Commons.Domain.Communication;
using Commons.Domain.DomainObjects;
using Commons.Domain.Messages;
using Commons.Domain.Messages.IntegrationEvents;
using Contatos.Cadastro.Api.Domain.Entities;
using Contatos.Cadastro.Api.Domain.Repositories;
using Contatos.Cadastro.Api.Domain.ValueObjects;
using MediatR;
using MessageBus;

namespace Contatos.Cadastro.Api.Application.Commands.Atualizar;

public class AtualizarContatoCommandHandler(IMessageBus bus, IContatoRepository repository)
    : CommandHandler, IRequestHandler<AtualizarContatoCommand, Result>
{
    public async Task<Result> Handle(AtualizarContatoCommand request, CancellationToken cancellationToken)
    {
        Contato? contato = await repository.ObterContatoPorIdAsync(request.Id);

        if (contato is null) throw new DomainException("Contato inválido.");

        var nome = new Nome(request.Nome, request.Sobrenome);

        Email? email = null;
        if (!string.IsNullOrWhiteSpace(request.Email)) email = new Email(request.Email);
        var telefones = request.Telefones.Select(t => new Telefone(t.Ddd, t.Numero, t.Tipo)).ToList();

        contato.AtualizarNome(nome);
        contato.AtualizarEmail(email);
        contato.AtualizarTelefones(telefones);

        var validationResult = contato.Validar();

        if (!validationResult.IsValid) return Result.Failure(validationResult.Errors);

        await bus.Publish(new CriarContatoIntegrationEvent
        {
            AggregateId = contato.Id,
            Nome = contato.Nome.PrimeiroNome,
            Sobrenome = contato.Nome.Sobrenome,
            Telefones = contato.Telefones.Select(t => new CriarContatoIntegrationEvent.Telefone
            {
                Ddd = t.Ddd,
                Numero = t.Numero,
                Tipo = t.Tipo.ToString(),
            }).ToList(),
            Email = contato.Email?.Endereco
        }, cancellationToken);

        return !validationResult.IsValid ? Result.Failure(validationResult.Errors) : Result.Success();
    }
}