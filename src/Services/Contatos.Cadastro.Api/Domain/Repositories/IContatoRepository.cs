using Commons.Domain.Data;
using Contatos.Cadastro.Api.Domain.Entities;

namespace Contatos.Cadastro.Api.Domain.Repositories;

public interface IContatoRepository : IRepository<Contato>
{
    void Adicionar(Contato contato);
    Task<Contato?> ObterContatoPorId(Guid id);
    void Atualizar(Contato contato);
    void Excluir(Contato contato);
}