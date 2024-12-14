namespace Commons.Domain.Messages.IntegrationEvents;

public class ContatoExcluidoIntegrationEvent : Event
{
    public string Nome { get; set; } = null!;
    public string? Sobrenome { get; set; }
    public string? Email { get; set; }
    public List<Telefone> Telefones { get; set; } = [];

    public class Telefone
    {
        public short Ddd { get; set; }
        public string Numero { get; set; } = null!;
        public string Tipo { get; set; } = null!;
    }
}