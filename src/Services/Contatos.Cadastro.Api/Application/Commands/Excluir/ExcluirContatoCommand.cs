using Commons.Domain.Communication;
using MediatR;

namespace Contatos.Cadastro.Api.Application.Commands.Excluir;

public class ExcluirContatoCommand : IRequest<Result>
{
    public Guid Id { get; set; }
}