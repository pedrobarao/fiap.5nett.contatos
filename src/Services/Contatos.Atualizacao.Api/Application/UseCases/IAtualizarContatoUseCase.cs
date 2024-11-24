using Commons.Domain.Communication;
using Contatos.Atualizacao.Api.Application.DTOs;

namespace Contatos.Atualizacao.Api.Application.UseCases;

public interface IAtualizarContatoUseCase
{
    Task<Result> ExecuteAsync(AtualizarContatoInput input);
}