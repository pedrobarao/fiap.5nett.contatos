using Contatos.Application.DTOs.Outputs;

namespace Contatos.Application.UseCases.Interfaces;

public interface IObterContatoUseCase
{
    Task<ObterContatoOutput?> ExecuteAsync(Guid id);
}