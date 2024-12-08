using Commons.Domain.Communication;
using Contatos.Consulta.Api.Application.DTOs;
using Contatos.Consulta.Api.Domain.Repositories;
using MapsterMapper;

namespace Contatos.Consulta.Api.Application.Queries;

public class ListarContatoQuery(IContatoRepository contatoRepository, IMapper mapper)
    : IListarContatoQuery
{
    public async Task<PagedResult<ObterContatoOutput>> ExecuteAsync(int pageSize, int pageIndex, string? query = null)
    {
        var contatosPaginados = await contatoRepository.ObterContatosPaginados(pageSize, pageIndex, query);
        return mapper.Map<PagedResult<ObterContatoOutput>>(contatosPaginados);
    }
}