using Commons.Domain.Communication;
using Contatos.Application.DTOs.Inputs;
using Contatos.Application.DTOs.Outputs;
using Contatos.Application.UseCases.Interfaces;
using Contatos.Domain.Entities;
using Contatos.Domain.Repositories;
using Contatos.Domain.ValueObjects;

namespace Contatos.Application.UseCases;

public class CadastrarContatoUseCase(IContatoRepository contatoRepository) : ICadastrarContatoUseCase
{
    private readonly Result<ContatoCriadoOutput> _result = new();

    public async Task<Result<ContatoCriadoOutput>> ExecuteAsync(NovoContatoInput input)
    {
        var nome = new Nome(input.Nome, input.Sobrenome);
        var email = new Email(input.Email!);
        var telefones = input.Telefones.Select(t => new Telefone(t.Ddd, t.Numero, t.Tipo)).ToList();
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