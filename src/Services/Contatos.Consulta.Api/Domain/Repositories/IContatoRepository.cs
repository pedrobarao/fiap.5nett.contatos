using Commons.Domain.Communication;
using Commons.Domain.Data;
using Contato = Contatos.Consulta.Api.Domain.Entities.Contato;

namespace Contatos.Consulta.Api.Domain.Repositories;

public interface IContatoRepository : IRepository<Contato>
{
    Task<Contato?> ObterContatoPorIdAsync(Guid id);
    Task<PagedResult<Contato>> ObterContatosPaginados(int pageSize, int pageIndex, string? query = null);
}