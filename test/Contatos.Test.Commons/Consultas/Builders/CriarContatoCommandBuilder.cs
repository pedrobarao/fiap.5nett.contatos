using Bogus;
using Contatos.Consulta.Api.Application.Commands;

namespace Contatos.Test.Commons.Consultas.Builders;

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
                new CriarContatoCommand.Telefone
                {
                    Ddd = t.Ddd, Numero = t.Numero, Tipo = t.Tipo
                }).ToList());
    }

    public CriarContatoCommand Build()
    {
        return _faker.Generate();
    }
}