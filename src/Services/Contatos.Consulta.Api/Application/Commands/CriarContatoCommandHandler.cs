using Commons.Domain.Communication;
using Commons.Domain.Messages;
using Contatos.Consulta.Api.Domain.Entities;
using Contatos.Consulta.Api.Domain.Repositories;
using Contatos.Consulta.Api.Domain.ValueObjects;
using MediatR;

namespace Contatos.Consulta.Api.Application.Commands;

public class CriarContatoCommandHandler(IContatoRepository repository)
    : CommandHandler, IRequestHandler<CriarContatoCommand, Result>
{
    public async Task<Result> Handle(CriarContatoCommand request, CancellationToken cancellationToken)
    {
        var contato = new Contato
        {
            Id = request.Id
        };

        if (!string.IsNullOrEmpty(request.Email)) contato.Email = new Email(request.Email);

        contato.Nome = new Nome(request.Nome, request.Sobrenome);

        contato.Telefones = request.Telefones
            .Select(t => new Telefone(t.Ddd, t.Numero, Enum.Parse<TipoTelefone>(t.Tipo))).ToList();

        await repository.Inserir(contato);

        return Result.Success();
    }
}