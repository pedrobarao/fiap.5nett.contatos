using Commons.Domain.Communication;
using MediatR;

namespace Contatos.Consulta.Api.Application.Commands;

public class CriarContatoCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string? Sobrenome { get; set; }
    public string? Email { get; set; }
    public List<Telefone> Telefones { get; set; } = [];

    public class Telefone
    {
        public short Ddd { get; set; }
        public string Numero { get; set; } = null!;
        public string Tipo { get; set; } = null!;
    }
}