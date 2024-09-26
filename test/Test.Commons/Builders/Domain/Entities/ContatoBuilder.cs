using Bogus;
using Contatos.Domain.Entities;
using Test.Commons.Builders.Domain.ValueObjects;

namespace Test.Commons.Builders.Domain.Entities;

public class ContatoBuilder
{
    private readonly Faker<Contato> _faker;

    public ContatoBuilder()
    {
        var nome = new NomeBuilder().Build();
        var telefones = new TelefoneBuilder().Build(3);
        var email = new EmailBuilder().Build();

        _faker = new Faker<Contato>("pt_BR")
            .CustomInstantiator(f => new Contato(
                nome,
                telefones,
                email
            ));
    }

    public ContatoBuilder ComNomeInvalido()
    {
        var nome = new NomeBuilder()
            .ComPrimeiroNome("")
            .Build();

        _faker.RuleFor(p => p.Nome, f => nome);

        return this;
    }

    public ContatoBuilder ComEmailInvalido()
    {
        var email = new EmailBuilder()
            .ComEndereco("invalido")
            .Build();

        _faker.RuleFor(p => p.Email, f => email);

        return this;
    }

    public ContatoBuilder ComTelefoneInvalido()
    {
        var telefones = new TelefoneBuilder()
            .ComDdd(0)
            .ComNumero("12345678910")
            .Build(1);

        _faker.CustomInstantiator(f => new Contato(
            new NomeBuilder().Build(),
            telefones,
            new EmailBuilder().Build()
        ));

        return this;
    }

    public Contato Build()
    {
        return _faker.Generate();
    }
}