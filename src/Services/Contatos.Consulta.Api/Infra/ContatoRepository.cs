using Commons.Domain.Communication;
using Contatos.Consulta.Api.Domain.Entities;
using Contatos.Consulta.Api.Domain.Repositories;
using MongoDB.Driver;

namespace Contatos.Consulta.Api.Infra;

public sealed class ContatoRepository : IContatoRepository
{
    private readonly IMongoCollection<Contato> _contatosCollection;

    public ContatoRepository(IMongoDatabase database)
    {
        _contatosCollection = database.GetCollection<Contato>("Contatos");
    }

    public async Task Inserir(Contato contato)
    {
        await _contatosCollection.InsertOneAsync(contato);
    }

    public async Task Atualizar(Contato contato)
    {
        var updateDefinition = Builders<Contato>.Update
            .Set(c => c.Nome, contato.Nome)
            .Set(c => c.Email, contato.Email)
            .Set(c => c.Telefones, contato.Telefones);

        await _contatosCollection.UpdateOneAsync(c => c.Id == contato.Id, updateDefinition);
    }

    public async Task Excluir(Guid id)
    {
        var filter = Builders<Contato>.Filter.Eq(c => c.Id, id);
        await _contatosCollection.DeleteOneAsync(filter);
    }

    public async Task<Contato?> ObterContatoPorId(Guid id)
    {
        var filter = Builders<Contato>.Filter.Eq(c => c.Id, id);
        return await _contatosCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<PagedResult<Contato>> ObterContatosPaginados(int pageSize, int pageIndex, string? query = "")
    {
        var skip = pageIndex * pageSize;
        var filters = string.IsNullOrEmpty(query)
            ? Builders<Contato>.Filter.Empty
            : Builders<Contato>.Filter.Where(c =>
                c.Nome.PrimeiroNome.ToLower().Contains(query.ToLower()) || (c.Nome.Sobrenome != null &&
                                                                            c.Nome.Sobrenome.ToLower()
                                                                                .Contains(query.ToLower())));

        var totalItems = await _contatosCollection.CountDocumentsAsync(filters);

        var contatos = await _contatosCollection
            .Find(filters)
            .Skip(skip)
            .Limit(pageSize)
            .ToListAsync();

        return new PagedResult<Contato>(contatos, (int)totalItems, pageIndex, pageSize, query);
    }
}