using Commons.Domain.Communication;
using Contatos.Application.DTOs.Outputs;

namespace Contatos.Application.UseCases.Interfaces;

public interface IListarContatoUseCase
{
    Task<PagedResult<ObterContatoOutput>> ExecuteAsync(int pageSize, int pageIndex, string? query = null);
}