using System.ComponentModel.DataAnnotations;
using Commons.Domain.Communication;
using Contatos.Cadastro.Api.Domain.ValueObjects;
using MediatR;

namespace Contatos.Cadastro.Api.Application.Commands.Criar;

public class CriarContatoCommand : IRequest<Result<Guid>>
{
    [Required(ErrorMessage = "A propriedade {0} é obrigatória")]
    public string Nome { get; set; } = null!;

    public string? Sobrenome { get; set; }

    [Required(ErrorMessage = "A propriedade {0} é obrigatória")]
    public List<TelefoneCriacao> Telefones { get; set; } = null!;

    public string? Email { get; set; }

    public class TelefoneCriacao
    {
        public short Ddd { get; set; }
        public string Numero { get; set; } = null!;
        public TipoTelefone Tipo { get; set; }
    }
}