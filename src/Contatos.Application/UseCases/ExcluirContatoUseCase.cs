using Contatos.Application.UseCases.Interfaces;
using Contatos.Domain.Repositories;

namespace Contatos.Application.UseCases;

public class ExcluirContatoUseCase(IContatoRepository contatoRepository) : IExcluirContatoUseCase
{
    public async Task ExecuteAsync(Guid id)
    {
        var contato = await contatoRepository.ObterContatoPorIdAsync(id);

        if (contato is null) return;

        contatoRepository.Excluir(contato);
        await contatoRepository.UnitOfWork.Commit();
    }
}