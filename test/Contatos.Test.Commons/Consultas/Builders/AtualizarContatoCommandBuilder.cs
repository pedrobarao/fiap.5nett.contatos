using Bogus;
using Contatos.Consulta.Api.Application.Commands;

namespace Contatos.Test.Commons.Consultas.Builders;

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
            new AtualizarContatoCommand.Telefone
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
}