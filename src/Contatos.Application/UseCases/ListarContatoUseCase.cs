using Commons.Domain.Communication;
using Contatos.Application.DTOs.Outputs;
using Contatos.Application.Mappings;
using Contatos.Application.UseCases.Interfaces;
using Contatos.Domain.Repositories;

namespace Contatos.Application.UseCases;

public class ListarContatoUseCase(IContatoRepository contatoRepository)
    : IListarContatoUseCase
{
    public async Task<PagedResult<ObterContatoOutput>> ExecuteAsync(int pageSize, int pageIndex, string? query = null)
    {
        var contatosPaginados = await contatoRepository.ObterContatosPaginados(pageSize, pageIndex, query);
        return ContatoMapping.ToPagedContatoResponse(contatosPaginados);
    }
}