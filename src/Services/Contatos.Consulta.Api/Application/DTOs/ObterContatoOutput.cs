namespace Contatos.Consulta.Api.Application.DTOs;

public class ObterContatoOutput
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string? Sobrenome { get; set; }
    public IList<TelefoneOutput> Telefones { get; set; } = null!;
    public string? Email { get; set; }

    public class TelefoneOutput
    {
        public short Ddd { get; set; }
        public string Numero { get; set; } = null!;
        public string Tipo { get; set; } = null!;
    }
}