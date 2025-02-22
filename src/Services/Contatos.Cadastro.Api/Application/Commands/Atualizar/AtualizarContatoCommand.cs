﻿using System.ComponentModel.DataAnnotations;
using Commons.Domain.Communication;
using Contatos.Cadastro.Api.Domain.ValueObjects;
using MediatR;

namespace Contatos.Cadastro.Api.Application.Commands.Atualizar;

public class AtualizarContatoCommand : IRequest<Result>
{
    [Required(ErrorMessage = "A propriedade {0} é obrigatória")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "A propriedade {0} é obrigatória")]
    public string Nome { get; set; } = null!;

    public string? Sobrenome { get; set; }
    public IList<TelefoneAtualizacao> Telefones { get; set; } = null!;
    public string? Email { get; set; }

    public class TelefoneAtualizacao
    {
        public short Ddd { get; set; }
        public string Numero { get; set; } = null!;
        public TipoTelefone Tipo { get; set; }
    }
}