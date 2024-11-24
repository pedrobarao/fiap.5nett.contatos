using Contatos.Consulta.Api.Application.DTOs;
using Contatos.Consulta.Api.Application.Mappings;
using Contatos.Consulta.Api.Domain;

namespace Contatos.Consulta.Api.Application.UseCases;

public class ObterContatoUseCase(IContatoRepository contatoRepository) : IObterContatoUseCase
{
    public async Task<ObterContatoOutput?> ExecuteAsync(Guid id)
    {
        var contato = await contatoRepository.ObterContatoPorIdAsync(id);
        if (contato is null) return null;
        return ContatoMapping.ToObterContatoOutput(contato);
    }
}