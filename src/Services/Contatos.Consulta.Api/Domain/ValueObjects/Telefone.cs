using Commons.Domain.Utils;

namespace Contatos.Consulta.Api.Domain.ValueObjects;

public record Telefone
{
    public Telefone(short ddd, string numero, string tipo)
    {
        Ddd = ddd;
        Numero = StringExtension.JustNumbers(numero);
        Tipo = tipo;
    }

    public short Ddd { get; init; }
    public string Numero { get; init; } = null!;
    public string Tipo { get; init; }

    public override string ToString()
    {
        return $"{Ddd}{Numero}";
    }
}