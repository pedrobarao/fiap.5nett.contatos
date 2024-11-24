using Commons.Domain.Data;
using Contatos.SharedKernel.Entities;

namespace Contatos.Cadastro.Api.Domain;

public interface IContatoRepository : IRepository<Contato>
{
    void Adicionar(Contato contato);
}