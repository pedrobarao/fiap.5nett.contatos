using Contatos.Application.DTOs.Outputs;
using Contatos.Application.Mappings;
using Contatos.Application.UseCases.Interfaces;
using Contatos.Domain.Repositories;

namespace Contatos.Application.UseCases;

public class ObterContatoUseCase(IContatoRepository contatoRepository) : IObterContatoUseCase
{
    public async Task<ObterContatoOutput?> ExecuteAsync(Guid id)
    {
        var contato = await contatoRepository.ObterContatoPorIdAsync(id);
        if (contato is null) return null;
        return ContatoMapping.ToObterContatoOutput(contato);
    }
}