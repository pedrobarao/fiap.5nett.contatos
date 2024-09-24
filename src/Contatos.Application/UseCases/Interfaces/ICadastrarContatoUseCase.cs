using Commons.Domain.Communication;
using Contatos.Application.DTOs.Inputs;
using Contatos.Application.DTOs.Outputs;

namespace Contatos.Application.UseCases.Interfaces;

public interface ICadastrarContatoUseCase
{
    public Task<Result<ContatoCriadoOutput>> ExecuteAsync(NovoContatoInput input);
}