namespace Contatos.Consulta.Api.Domain.ValueObjects;

public record Nome
{
    public Nome(string primeiroNome, string? sobrenome = null)
    {
        PrimeiroNome = primeiroNome;
        Sobrenome = sobrenome;
    }

    public string PrimeiroNome { get; private set; } = null!;
    public string? Sobrenome { get; private set; }

    public override string ToString()
    {
        return $"{PrimeiroNome} {Sobrenome}";
    }
}