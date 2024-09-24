using System.ComponentModel.DataAnnotations;
using Contatos.Domain.ValueObjects;

namespace Contatos.Application.DTOs.Inputs;

public class AtualizarContatoInput
{
    [Required(ErrorMessage = "A propriedade {0} é obrigatória")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "A propriedade {0} é obrigatória")]
    public string Nome { get; set; } = null!;

    public string? Sobrenome { get; set; }
    public IList<Telefone> Telefones { get; set; } = null!;
    public string? Email { get; set; }
}