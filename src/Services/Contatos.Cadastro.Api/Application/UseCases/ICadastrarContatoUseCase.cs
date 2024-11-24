using Commons.Domain.Communication;
using Contatos.Cadastro.Api.Application.DTOs.Inputs;
using Contatos.Cadastro.Api.Application.DTOs.Outputs;

namespace Contatos.Cadastro.Api.Application.UseCases;

public interface ICadastrarContatoUseCase
{
    public Task<Result<ContatoCriadoOutput>> ExecuteAsync(NovoContatoInput input);
}