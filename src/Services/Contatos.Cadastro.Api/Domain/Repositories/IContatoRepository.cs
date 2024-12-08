using Commons.Domain.Communication;
using Commons.Domain.Data;
using Contatos.Cadastro.Api.Domain.Entities;

namespace Contatos.Cadastro.Api.Domain.Repositories;

public interface IContatoRepository : IRepository<Contato>
{
    void Adicionar(Contato contato);
    Task<Contato?> ObterContatoPorIdAsync(Guid id);
    Task<PagedResult<Contato>> ObterContatosPaginados(int pageSize, int pageIndex, string? query = null);
    void Atualizar(Contato contato);
    void Excluir(Contato contato);
}