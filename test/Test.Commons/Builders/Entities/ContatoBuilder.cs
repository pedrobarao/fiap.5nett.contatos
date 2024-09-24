using Bogus;
using Contatos.Domain.Entities;
using Contatos.Domain.ValueObjects;
using Test.Commons.Builders.ValueObjects;

namespace Test.Commons.Builders.Entities;

public class ContatoBuilder
{
    private readonly Faker<Contato> _faker = new()
    {
        Locale = "pt_BR"
    };
    
    public ContatoBuilder()
    {
        _faker.RuleFor(c => c.Id, f => f.Random.Guid());
        
        _faker.RuleFor(c => c.Nome, f => new NomeBuilder().Build());
        _faker.RuleFor(c => c.Email, f => new EmailBuilder().Build());
        
        _faker.RuleFor(c => c.Telefones, f => new List<Telefone>
        {
            new(f.Random.Short(11, 99), f.Person.Phone, f.PickRandom<TipoTelefone>())
        });
    }

    public Contato Build()
    {
        return _faker.Generate();
    }
}