using Commons.Domain.Data;
using Contatos.Cadastro.Api.Domain.Entities;
using Contatos.Cadastro.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Contatos.Cadastro.Api.Infra.Data.Repositories;

public sealed class ContatoRepository(ContatoDbContext context) : IContatoRepository
{
    public IUnitOfWork UnitOfWork => context!;

    public void Adicionar(Contato contato)
    {
        context.Contatos.Add(contato);
    }

    public async Task<Contato?> ObterContatoPorId(Guid id)
    {
        return await context.Contatos.FirstOrDefaultAsync(p => p.Id == id);
    }

    public void Atualizar(Contato contato)
    {
        context.Contatos.Update(contato);
    }

    public void Excluir(Contato contato)
    {
        context.Contatos.Remove(contato);
    }

    public void Dispose()
    {
        context?.Dispose();
    }
}