using Commons.Domain.Communication;
using Commons.Domain.DomainObjects;
using Contatos.Application.DTOs.Inputs;
using Contatos.Application.UseCases.Interfaces;
using Contatos.Domain.Repositories;
using Contatos.Domain.ValueObjects;

namespace Contatos.Application.UseCases;

public class AtualizarContatoUseCase(IContatoRepository contatoRepository) : IAtualizarContatoUseCase
{
    private readonly Result _result = new();

    public async Task<Result> ExecuteAsync(AtualizarContatoInput input)
    {
        var contato = await contatoRepository.ObterContatoPorIdAsync(input.Id);

        if (contato is null) throw new DomainException("Contato inválido.");

        var nome = new Nome(input.Nome, input.Sobrenome);

        Email? email = null;
        if (!string.IsNullOrWhiteSpace(input.Email)) email = new Email(input.Email);
        var telefones = input.Telefones.Select(t => new Telefone(t.Ddd, t.Numero, t.Tipo)).ToList();

        contato.AtualizarNome(nome);
        contato.AtualizarEmail(email);
        contato.AtualizarTelefones(telefones);

        var validationResult = contato.Validar();

        if (!validationResult.IsValid)
        {
            _result.Errors.AddRange(validationResult.Errors);
            return _result;
        }

        try
        {
            contatoRepository.Atualizar(contato);
            await contatoRepository.UnitOfWork.Commit();
            return _result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}