using Commons.Domain.Data;
using Contatos.SharedKernel.Entities;

namespace Contatos.Atualizacao.Api.Domain;

public interface IContatoRepository : IRepository<Contato>
{
    void Atualizar(Contato contato);
}