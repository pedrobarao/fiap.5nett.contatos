using System.Diagnostics.CodeAnalysis;
using Commons.Domain.Communication;
using Commons.Domain.Utils;

namespace Contatos.Consulta.Api.Domain.ValueObjects;

public record Telefone
{
    [ExcludeFromCodeCoverage]
    protected Telefone()
    {
    }

    public Telefone(short ddd, string numero, TipoTelefone tipo)
    {
        Ddd = ddd;
        Numero = StringExtension.JustNumbers(numero);
        Tipo = tipo;
    }

    public short Ddd { get; init; }
    public string Numero { get; init; } = null!;
    public TipoTelefone Tipo { get; init; }

    public override string ToString()
    {
        return $"{Ddd}{Numero}";
    }

    public ValidationResult Validar()
    {
        var result = new ValidationResult();
        if (!ValidarDdd(Ddd)) result.AddError(Error.DddInvalido.WithMessageParam(Ddd));
        if (!ValidarNumero(Numero, Tipo)) result.AddError(Error.TelefoneInvalido.WithMessageParam(Numero));
        return result;
    }

    public static bool ValidarDdd(short ddd)
    {
        return ddd is >= 11 and <= 99;
    }

    public static bool ValidarNumero(string numero, TipoTelefone tipo)
    {
        if (string.IsNullOrEmpty(numero)) return false;
        numero = StringExtension.JustNumbers(numero);
        if (numero.Length != 8 && numero.Length != 9) return false;

        if (tipo == TipoTelefone.Celular) return numero.Length == 9 && numero.StartsWith('9');

        return numero.Length == 8;
    }
}