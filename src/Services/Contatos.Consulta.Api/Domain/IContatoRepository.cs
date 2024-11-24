using Commons.Domain.Communication;
using Commons.Domain.Data;
using Contatos.SharedKernel.Entities;

namespace Contatos.Consulta.Api.Domain;

public interface IContatoRepository : IRepository<Contato>
{
    Task<Contato?> ObterContatoPorIdAsync(Guid id);
    Task<PagedResult<Contato>> ObterContatosPaginados(int pageSize, int pageIndex, string? query = null);
}