using Commons.Domain.Communication;
using Contatos.Cadastro.Api.Application.DTOs.Inputs;
using Contatos.Cadastro.Api.Application.DTOs.Outputs;
using Contatos.Cadastro.Api.Domain;
using Contatos.SharedKernel.Entities;
using Contatos.SharedKernel.ValueObjects;

namespace Contatos.Cadastro.Api.Application.UseCases;

public class CadastrarContatoUseCase(IContatoRepository contatoRepository) : ICadastrarContatoUseCase
{
    private readonly Result<ContatoCriadoOutput> _result = new();

    public async Task<Result<ContatoCriadoOutput>> ExecuteAsync(NovoContatoInput input)
    {
        var nome = new Nome(input.Nome, input.Sobrenome);
        var email = new Email(input.Email!);
        var telefones = input.Telefones.Select(t => new Telefone(t.Ddd, t.Numero, t.Tipo)).ToList(); // TODO: Princípio da ocultação
        var contato = new Contato(nome, telefones, email);

        var validationResult = contato.Validar();

        if (!validationResult.IsValid)
        {
            _result.Errors.AddRange(validationResult.Errors);
            return _result;
        }

        contatoRepository.Adicionar(contato);
        await contatoRepository.UnitOfWork.Commit();
        _result.SetData(new ContatoCriadoOutput { Id = contato.Id });
        return _result;
    }
}