using Contatos.Consulta.Api.Domain.ValueObjects;

namespace Contatos.Consulta.Api.Domain.Entities;

public class Contato
{
    private readonly List<Telefone> _telefones;

    public Contato(Nome nome, List<Telefone> telefone, Email? email)
    {
        Nome = nome;
        _telefones = telefone;
        Email = email;
    }

    public Guid Id { get; set; }
    public Nome Nome { get; private set; }
    public Email? Email { get; private set; }
    public IReadOnlyCollection<Telefone> Telefones => _telefones;
}