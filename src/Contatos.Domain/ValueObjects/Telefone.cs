using System.Diagnostics.CodeAnalysis;
using Commons.Domain.Communication;
using Infra.CrossCutting.Utils;

namespace Contatos.Domain.ValueObjects;

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
        if (!ValidarDdd(Ddd)) result.Errors.Add($"{Ddd} não é um DDD válido.");
        if (!ValidarNumero(Numero, Tipo)) result.Errors.Add($"{Numero} não é um número de telefone válido.");
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