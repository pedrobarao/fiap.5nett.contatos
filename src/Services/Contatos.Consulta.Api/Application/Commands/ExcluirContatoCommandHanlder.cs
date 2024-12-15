using Commons.Domain.Communication;
using Commons.Domain.Messages;
using Contatos.Consulta.Api.Domain.Repositories;
using MediatR;

namespace Contatos.Consulta.Api.Application.Commands;

public class ExcluirContatoCommandHanlder(IContatoRepository repository)
    : CommandHandler, IRequestHandler<ExcluirContatoCommand, Result>
{
    public async Task<Result> Handle(ExcluirContatoCommand request, CancellationToken cancellationToken)
    {
        await repository.Excluir(request.Id);
        return Result.Success();
    }
}