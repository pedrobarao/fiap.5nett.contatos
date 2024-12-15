using System.Diagnostics.CodeAnalysis;
using Commons.Domain.Communication;

namespace Contatos.Cadastro.Api.Domain.ValueObjects;

public record Nome
{
    [ExcludeFromCodeCoverage]
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

        if (string.IsNullOrWhiteSpace(PrimeiroNome)) result.AddError(Error.PrimeiroNomeObrigatorio);

        return result;
    }
}