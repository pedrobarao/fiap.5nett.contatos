using Commons.Domain.Communication;
using Contatos.Consulta.Api.Application.DTOs;

namespace Contatos.Consulta.Api.Application.Queries;

public interface IListarContatoQuery
{
    Task<PagedResult<ObterContatoOutput>> ExecuteAsync(int pageSize, int pageIndex, string? query = null);
}