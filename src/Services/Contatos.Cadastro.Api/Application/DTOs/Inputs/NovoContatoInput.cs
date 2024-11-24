﻿using System.ComponentModel.DataAnnotations;
using Contatos.SharedKernel.ValueObjects;

namespace Contatos.Cadastro.Api.Application.DTOs.Inputs;

public class NovoContatoInput
{
    [Required(ErrorMessage = "A propriedade {0} é obrigatória")]
    public string Nome { get; set; } = null!;

    public string? Sobrenome { get; set; }

    [Required(ErrorMessage = "A propriedade {0} é obrigatória")]
    public List<Telefone> Telefones { get; set; } = null!;

    public string? Email { get; set; }
}