using Bogus;
using Contatos.Cadastro.Api.Application.Commands.Atualizar;

namespace Contatos.Test.Commons.Cadastros.Builders;

public class AtualizarContatoCommandBuilder
{
    private readonly Faker<AtualizarContatoCommand> _faker = new()
    {
        Locale = "pt_BR"
    };

    public AtualizarContatoCommandBuilder()
    {
        _faker.RuleFor(c => c.Id, f => f.Random.Guid());
        _faker.RuleFor(c => c.Nome, f => f.Person.FirstName);
        _faker.RuleFor(c => c.Sobrenome, f => f.Person.LastName);
        _faker.RuleFor(c => c.Email, f => f.Person.Email);

        var telefones = new TelefoneBuilder().Build(3);
        _faker.RuleFor(c => c.Telefones, f => telefones.Select(t =>
            new AtualizarContatoCommand.TelefoneAtualizacao
            {
                Ddd = t.Ddd, Numero = t.Numero, Tipo = t.Tipo
            }).ToList());
    }

    public AtualizarContatoCommand Build()
    {
        return _faker.Generate();
    }

    public AtualizarContatoCommandBuilder ComIdContato(Guid id)
    {
        _faker.RuleFor(c => c.Id, id);
        return this;
    }

    public AtualizarContatoCommandBuilder ComNomeInvalido()
    {
        _faker.RuleFor(c => c.Nome, "");

        return this;
    }

    public AtualizarContatoCommandBuilder ComEmailInvalido()
    {
        _faker.RuleFor(c => c.Email, "invalido");

        return this;
    }

    public AtualizarContatoCommandBuilder ComTelefoneInvalido()
    {
        var telefones = new TelefoneBuilder()
            .ComDdd(0)
            .ComNumero("12345678910")
            .Build(1);

        _faker.RuleFor(c => c.Telefones, telefones.Select(t =>
            new AtualizarContatoCommand.TelefoneAtualizacao
            {
                Ddd = t.Ddd, Numero = t.Numero, Tipo = t.Tipo
            }).ToList());

        return this;
    }
}