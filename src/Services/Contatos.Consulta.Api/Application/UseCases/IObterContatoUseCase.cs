using Contatos.Consulta.Api.Application.DTOs;

namespace Contatos.Consulta.Api.Application.UseCases;

public interface IObterContatoUseCase
{
    Task<ObterContatoOutput?> ExecuteAsync(Guid id);
}