using Contatos.Consulta.Api.Application.DTOs;

namespace Contatos.Consulta.Api.Application.Queries;

public interface IObterContatoQuery
{
    Task<ObterContatoOutput?> ExecuteAsync(Guid id);
}