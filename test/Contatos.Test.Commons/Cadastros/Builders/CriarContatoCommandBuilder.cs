using Bogus;
using Contatos.Cadastro.Api.Application.Commands.Criar;

namespace Contatos.Test.Commons.Cadastros.Builders;

public class CriarContatoCommandBuilder
{
    private readonly Faker<CriarContatoCommand> _faker = new()
    {
        Locale = "pt_BR"
    };

    public CriarContatoCommandBuilder()
    {
        _faker.RuleFor(c => c.Nome, f => f.Person.FirstName);
        _faker.RuleFor(c => c.Sobrenome, f => f.Person.LastName);
        _faker.RuleFor(c => c.Email, f => f.Person.Email);

        var telefones = new TelefoneBuilder().Build(3);
        _faker.RuleFor(c => c.Telefones,
            f => telefones.Select(t =>
                new CriarContatoCommand.TelefoneCriacao
                {
                    Ddd = t.Ddd, Numero = t.Numero, Tipo = t.Tipo
                }).ToList());
    }

    public CriarContatoCommand Build()
    {
        return _faker.Generate();
    }

    public CriarContatoCommandBuilder ComNomeInvalido()
    {
        _faker.RuleFor(c => c.Nome, "");

        return this;
    }

    public CriarContatoCommandBuilder ComEmailInvalido()
    {
        _faker.RuleFor(c => c.Email, "invalido");

        return this;
    }

    public CriarContatoCommandBuilder ComTelefoneInvalido()
    {
        var telefones = new TelefoneBuilder()
            .ComDdd(0)
            .ComNumero("12345678910")
            .Build(1);

        _faker.RuleFor(c => c.Telefones, telefones.Select(t =>
            new CriarContatoCommand.TelefoneCriacao
            {
                Ddd = t.Ddd, Numero = t.Numero, Tipo = t.Tipo
            }).ToList());

        return this;
    }
}