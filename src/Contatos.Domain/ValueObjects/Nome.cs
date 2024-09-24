using Commons.Domain.Communication;

namespace Contatos.Domain.ValueObjects;

public class Nome
{
    protected Nome()
    {
    }

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

    public ValidationResult Validar()
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(PrimeiroNome)) result.Errors.Add("O primeiro nome é obrigatório");

        return result;
    }
}