using Commons.Domain.Communication;
using Commons.Domain.DomainObjects;
using Contatos.Domain.ValueObjects;

namespace Contatos.Domain.Entities;

public class Contato : Entity, IAggregateRoot
{
    private List<Telefone> _telefones = null!;

    protected Contato()
    {
    }

    public Contato(Nome nome, List<Telefone> telefone, Email? email)
    {
        Nome = nome;
        _telefones = telefone;
        Email = email;
    }

    public Contato(Guid id, Nome nome, List<Telefone> telefone, Email? email)
    {
        Id = id;
        Nome = nome;
        _telefones = telefone;
        Email = email;
    }

    public Nome Nome { get; private set; } = null!;
    public Email? Email { get; private set; }
    public IReadOnlyCollection<Telefone> Telefones => _telefones;

    public ValidationResult Validar()
    {
        var result = new ValidationResult();
        ValidarNome(ref result);
        ValidarEmail(ref result);
        ValidarListaTelefones(ref result);
        return result;
    }

    private void ValidarNome(ref ValidationResult result)
    {
        var resultNome = Nome.Validar();
        if (!resultNome.IsValid) result.Errors.AddRange(resultNome.Errors);
    }

    private void ValidarEmail(ref ValidationResult result)
    {
        var resultEmail = Email?.Validar();
        if (resultEmail is not null && !resultEmail.IsValid) result.Errors.AddRange(resultEmail.Errors);
    }

    private void ValidarListaTelefones(ref ValidationResult result)
    {
        foreach (var telefone in Telefones)
        {
            var telefoneResult = telefone.Validar();

            if (!telefoneResult.IsValid) result.Errors.AddRange(telefone.Validar().Errors);
        }
    }

    public void AtualizarNome(Nome nome)
    {
        Nome = nome;
    }

    public void AtualizarTelefones(List<Telefone> telefones)
    {
        _telefones = telefones;
    }

    public void AtualizarEmail(Email? email)
    {
        Email = email;
    }
}