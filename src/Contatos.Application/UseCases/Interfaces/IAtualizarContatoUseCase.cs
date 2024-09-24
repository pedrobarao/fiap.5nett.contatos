using Commons.Domain.Communication;
using Contatos.Application.DTOs.Inputs;

namespace Contatos.Application.UseCases.Interfaces;

public interface IAtualizarContatoUseCase
{
    Task<Result> ExecuteAsync(AtualizarContatoInput input);
}