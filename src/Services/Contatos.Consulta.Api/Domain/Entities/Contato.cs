using Contatos.Consulta.Api.Domain.ValueObjects;

namespace Contatos.Consulta.Api.Domain.Entities;

public class Contato
{
    public Guid Id { get; set; }
    public Nome Nome { get; set; } = null!;
    public Email? Email { get; set; }
    public List<Telefone> Telefones { get; set; } = null!;
}