using Commons.Domain.Communication;
using MediatR;

namespace Contatos.Consulta.Api.Application.Commands;

public class ExcluirContatoCommand : IRequest<Result>
{
    public Guid Id { get; set; }
}