namespace Contatos.Consulta.Api.Domain.ValueObjects;

public record Email
{
    public Email(string endereco)
    {
        Endereco = endereco;
    }

    public string? Endereco { get; private set; }

    public override string? ToString()
    {
        return Endereco;
    }
}