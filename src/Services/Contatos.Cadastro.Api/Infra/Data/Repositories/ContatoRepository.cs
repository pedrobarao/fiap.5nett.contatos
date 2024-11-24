using Commons.Domain.Data;
using Contatos.Cadastro.Api.Domain;
using Contatos.SharedKernel.Entities;

namespace Contatos.Cadastro.Api.Infra.Data.Repositories;

public sealed class ContatoRepository(ContatoDbContext context) : IContatoRepository
{
    public IUnitOfWork UnitOfWork => context!;

    public void Adicionar(Contato contato)
    {
        context.Contatos.Add(contato);
    }
    
    public void Dispose()
    {
        context?.Dispose();
    }
}