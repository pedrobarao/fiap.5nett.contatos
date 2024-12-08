using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Commons.Domain.Communication;

namespace Contatos.Consulta.Api.Domain.ValueObjects;

public record Email
{
    public const int MaxLength = 254;

    [ExcludeFromCodeCoverage]
    protected Email()
    {
    }

    public Email(string endereco)
    {
        Endereco = endereco;
    }

    public string? Endereco { get; private set; }

    public override string? ToString()
    {
        return Endereco;
    }

    public ValidationResult Validar()
    {
        var result = new ValidationResult();

        if (!ValidarFormatacao(Endereco)) result.AddError(Error.EmailInvalido);

        return result;
    }

    public static bool ValidarFormatacao(string? email)
    {
        if (string.IsNullOrWhiteSpace(email)) return true;

        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        if (!emailRegex.IsMatch(email)) return false;

        return true;
    }
}