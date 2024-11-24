using Commons.Domain.Communication;
using Commons.Domain.Data;
using Contatos.Consulta.Api.Domain;
using Contatos.SharedKernel.Entities;
using Contatos.SharedKernel.ValueObjects;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Contatos.Consulta.Api.Infra.Data.Repositories;

public sealed class ContatoRepository(ContatoDbContext context) : IContatoRepository
{
    public IUnitOfWork UnitOfWork => context!;

public async Task<Contato?> ObterContatoPorIdAsync(Guid id)
    {
        return await context.Contatos.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<PagedResult<Contato>> ObterContatosPaginados(int pageSize, int pageIndex, string? query = null)
    {
        const string sql = @$"SELECT ""Contatos"".""Id"" AS ""{nameof(Contato.Id)}"",
                                    ""Contatos"".""PrimeiroNome"" AS ""{nameof(Nome.PrimeiroNome)}"",
                                    ""Contatos"".""Sobrenome"" AS ""{nameof(Nome.Sobrenome)}"",
                                    ""Contatos"".""Email"" AS ""{nameof(Email.Endereco)}"",
                                    ""Telefones"".""Ddd"" AS ""{nameof(Telefone.Ddd)}"",
                                    ""Telefones"".""Numero"" AS ""{nameof(Telefone.Numero)}"",
                                    ""Telefones"".""Tipo"" AS ""{nameof(Telefone.Tipo)}""
                               FROM ""Contatos"" AS ""Contatos""
                               INNER JOIN ""Telefones"" AS ""Telefones"" ON ""Contatos"".""Id"" = ""Telefones"".""ContatoId""
                              WHERE (@query IS NULL OR UPPER(""PrimeiroNome"") LIKE '%' || @query || '%')
                                OR (@query IS NULL OR UPPER(""Sobrenome"") LIKE '%' || @query || '%')
                                OR (@query IS NULL OR ""Telefones"".""Ddd""::text = @query)
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
                splitOn: "Id,PrimeiroNome,Endereco,Ddd");

        var totalItems = await context.Database.GetDbConnection()
            .QueryFirstOrDefaultAsync<int>(count, queryParams);

        return new PagedResult<Contato>(contatos?.Distinct(), totalItems, pageIndex, pageSize, query);
    }
    
    public void Dispose()
    {
        context?.Dispose();
    }
}