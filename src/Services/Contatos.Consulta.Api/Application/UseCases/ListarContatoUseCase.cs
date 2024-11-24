using Commons.Domain.Communication;
using Contatos.Consulta.Api.Application.DTOs;
using Contatos.Consulta.Api.Application.Mappings;
using Contatos.Consulta.Api.Domain;

namespace Contatos.Consulta.Api.Application.UseCases;

public class ListarContatoUseCase(IContatoRepository contatoRepository)
    : IListarContatoUseCase
{
    public async Task<PagedResult<ObterContatoOutput>> ExecuteAsync(int pageSize, int pageIndex, string? query = null)
    {
        try
        {
            var contatosPaginados = await contatoRepository.ObterContatosPaginados(pageSize, pageIndex, query);
            return ContatoMapping.ToPagedContatoResponse(contatosPaginados);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}