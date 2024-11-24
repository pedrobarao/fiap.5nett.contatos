using Commons.Domain.Data;
using Contatos.Atualizacao.Api.Domain;
using Contatos.SharedKernel.Entities;

namespace Contatos.Atualizacao.Api.Infra.Data.Repositories;

public sealed class ContatoRepository(ContatoDbContext context) : IContatoRepository
{
    public IUnitOfWork UnitOfWork => context!;

    public void Atualizar(Contato contato)
    {
        context.Contatos.Update(contato);
    }
    
    public void Dispose()
    {
        context?.Dispose();
    }
}