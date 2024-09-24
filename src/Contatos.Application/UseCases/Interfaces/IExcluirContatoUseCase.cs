namespace Contatos.Application.UseCases.Interfaces;

public interface IExcluirContatoUseCase
{
    Task ExecuteAsync(Guid id);
}