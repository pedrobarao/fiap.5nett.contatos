using Commons.Domain.Communication;
using Contatos.Application.DTOs.Outputs;
using Contatos.Domain.Entities;

namespace Contatos.Application.Mappings;

public class ContatoMapping
{
    public static PagedResult<ObterContatoOutput> ToPagedContatoResponse(PagedResult<Contato> contatosPaginados)
    {
        var contatosResponse = contatosPaginados.Items.Select(ToObterContatoOutput).ToList();

        return new PagedResult<ObterContatoOutput>
        (
            contatosResponse,
            contatosPaginados.TotalItems,
            contatosPaginados.PageIndex,
            contatosPaginados.PageSize,
            contatosPaginados.Filter
        );
    }

    public static ObterContatoOutput ToObterContatoOutput(Contato contato)
    {
        return new ObterContatoOutput
        {
            Id = contato.Id,
            Nome = contato.Nome.PrimeiroNome,
            Sobrenome = contato.Nome.Sobrenome,
            Email = contato.Email?.Endereco,
            Telefones = contato.Telefones.Select(t => new ObterContatoOutput.TelefoneOutput
            {
                Numero = t.Numero,
                Tipo = t.Tipo.ToString(),
                Ddd = t.Ddd
            }).ToList()
        };
    }
}