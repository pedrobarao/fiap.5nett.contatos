using Bogus;

namespace Contatos.Atualizacao.UnitTest.Builders;

public class AtualizarContatoInputBuilder
{
    private readonly Faker<AtualizarContatoInput> _faker = new()
    {
        Locale = "pt_BR"
    };

    public AtualizarContatoInputBuilder()
    {
        _faker.RuleFor(c => c.Id, f => f.Random.Guid());
        _faker.RuleFor(c => c.Nome, f => f.Person.FirstName);
        _faker.RuleFor(c => c.Sobrenome, f => f.Person.LastName);
        _faker.RuleFor(c => c.Email, f => f.Person.Email);

        var telefones = new TelefoneBuilder().Build(3);
        _faker.RuleFor(c => c.Telefones, f => telefones);
    }

    public AtualizarContatoInput Build()
    {
        return _faker.Generate();
    }

    public AtualizarContatoInputBuilder ComIdContato(Guid id)
    {
        _faker.RuleFor(c => c.Id, id);
        return this;
    }

    public AtualizarContatoInputBuilder ComNomeInvalido()
    {
        _faker.RuleFor(c => c.Nome, "");

        return this;
    }

    public AtualizarContatoInputBuilder ComEmailInvalido()
    {
        _faker.RuleFor(c => c.Email, "invalido");

        return this;
    }

    public AtualizarContatoInputBuilder ComTelefoneInvalido()
    {
        var telefones = new TelefoneBuilder()
            .ComDdd(0)
            .ComNumero("12345678910")
            .Build(1);

        _faker.RuleFor(c => c.Telefones, telefones);

        return this;
    }
}