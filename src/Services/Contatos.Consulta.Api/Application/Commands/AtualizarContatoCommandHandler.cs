using Commons.Domain.Communication;
using Commons.Domain.Messages;
using Contatos.Consulta.Api.Domain.Entities;
using Contatos.Consulta.Api.Domain.Repositories;
using Contatos.Consulta.Api.Domain.ValueObjects;
using MediatR;

namespace Contatos.Consulta.Api.Application.Commands;

public class AtualizarContatoCommandHandler(IContatoRepository repository)
    : CommandHandler, IRequestHandler<AtualizarContatoCommand, Result>
{
    public async Task<Result> Handle(AtualizarContatoCommand request, CancellationToken cancellationToken)
    {
        var contato = new Contato
        {
            Id = request.Id
        };

        if (!string.IsNullOrEmpty(request.Email)) contato.Email = new Email(request.Email);

        contato.Nome = new Nome(request.Nome, request.Sobrenome);

        contato.Telefones = request.Telefones
            .Select(t => new Telefone(t.Ddd, t.Numero, t.Tipo)).ToList();

        await repository.Atualizar(contato);

        return Result.Success();
    }
}