using Commons.Domain.Communication;
using Commons.Domain.Data;
using Contatos.Domain.Entities;
using Contatos.Domain.Repositories;
using Contatos.Domain.ValueObjects;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Contatos.Infra.Data.Repositories;

public sealed class ContatoRepository(ContatoDbContext context) : IContatoRepository
{
    public IUnitOfWork UnitOfWork => context!;

    public void Adicionar(Contato contato)
    {
        context.Contatos.Add(contato);
    }

    public async Task<Contato?> ObterContatoPorIdAsync(Guid id)
    {
        return await context.Contatos.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<PagedResult<Contato>> ObterContatosPaginados(int pageSize, int pageIndex, string? query = null)
    {
        const string sql = @"SELECT ""Contatos"".""Id"",
                                    ""Contatos"".""PrimeiroNome"",
                                    ""Contatos"".""Sobrenome"",
                                    ""Contatos"".""Email"" AS ""Endereco"",
                                    ""Telefones"".""ContatoId"",
                                    ""Telefones"".""Ddd"",
                                    ""Telefones"".""Numero"",
                                    ""Telefones"".""Tipo""
                               FROM ""Contatos"" AS ""Contatos""
                               INNER JOIN ""Telefones"" AS ""Telefones"" ON ""Contatos"".""Id"" = ""Telefones"".""ContatoId""
                              WHERE (@query IS NULL OR UPPER(""PrimeiroNome"") LIKE '%' || @query || '%')
                                OR (@query IS NULL OR UPPER(""Sobrenome"") LIKE '%' || @query || '%')
                              ORDER BY ""PrimeiroNome""
                              LIMIT @pageSize OFFSET @pageIndex * @pageSize";

        const string count = @"SELECT COUNT(""Id"") FROM ""Contatos"" 
                              WHERE (@query IS NULL OR UPPER(""PrimeiroNome"") LIKE '%' || @query || '%')
                                OR (@query IS NULL OR UPPER(""Sobrenome"") LIKE '%' || @query || '%')";

        var queryParams = new { pageSize, pageIndex, query = query?.ToUpper() };

        var contatoDictionary = new Dictionary<Guid, Contato>();

        var contatos = await context.Database.GetDbConnection()
            .QueryAsync<Contato, Nome, Email, Telefone, Contato>(sql, (contato, nome, email, telefone) =>
                {
                    if (!contatoDictionary.TryGetValue(contato.Id, out var contatoEntry))
                    {
                        contatoEntry = contato;
                        contatoEntry.AtualizarTelefones(new List<Telefone>());
                        contatoEntry.AtualizarNome(nome);
                        contatoEntry.AtualizarEmail(email);
                        contatoDictionary.Add(contatoEntry.Id, contatoEntry);
                    }

                    var telefones = contatoEntry.Telefones.ToList();
                    telefones.Add(telefone);
                    contatoEntry.AtualizarTelefones(telefones);

                    return contatoEntry;
                }, queryParams,
                splitOn: "Id,PrimeiroNome,Endereco,ContatoId");

        var totalItems = await context.Database.GetDbConnection()
            .QueryFirstOrDefaultAsync<int>(count, queryParams);

        return new PagedResult<Contato>(contatos?.Distinct(), totalItems, pageIndex, pageSize, query);
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