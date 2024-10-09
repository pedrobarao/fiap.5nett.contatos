using Bogus;
using Contatos.Application.DTOs.Inputs;
using Test.Commons.Builders.Domain.ValueObjects;

namespace Test.Commons.Builders.Application.DTOs.Inputs;

public class NovoContatoInputBuilder
{
    private readonly Faker<NovoContatoInput> _faker = new()
    {
        Locale = "pt_BR"
    };

    public NovoContatoInputBuilder()
    {
        _faker.RuleFor(c => c.Nome, f => f.Person.FirstName);
        _faker.RuleFor(c => c.Sobrenome, f => f.Person.LastName);
        _faker.RuleFor(c => c.Email, f => f.Person.Email);

        var telefones = new TelefoneBuilder().Build(3);
        _faker.RuleFor(c => c.Telefones, f => telefones);
    }

    public NovoContatoInput Build()
    {
        return _faker.Generate();
    }

    public NovoContatoInputBuilder ComNomeInvalido()
    {
        _faker.RuleFor(c => c.Nome, "");

        return this;
    }

    public NovoContatoInputBuilder ComEmailInvalido()
    {
        _faker.RuleFor(c => c.Email, "invalido");

        return this;
    }

    public NovoContatoInputBuilder ComTelefoneInvalido()
    {
        var telefones = new TelefoneBuilder()
            .ComDdd(0)
            .ComNumero("12345678910")
            .Build(1);

        _faker.RuleFor(c => c.Telefones, telefones);

        return this;
    }
}