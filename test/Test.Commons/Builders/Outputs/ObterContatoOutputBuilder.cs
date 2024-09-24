using Bogus;
using Contatos.Application.DTOs.Outputs;
using Contatos.Domain.ValueObjects;

namespace Test.Commons.Builders.Outputs;

public class ObterContatoOutputBuilder
{
    private readonly Faker<ObterContatoOutput> _faker = new()
    {
        Locale = "pt_BR"
    };

    public ObterContatoOutputBuilder()
    {
        _faker.RuleFor(c => c.Id, f => f.Random.Guid());
        _faker.RuleFor(c => c.Nome, f => f.Person.FirstName);
        _faker.RuleFor(c => c.Sobrenome, f => f.Person.LastName);
        _faker.RuleFor(c => c.Email, f => f.Person.Email);
        _faker.RuleFor(c => c.Telefones, f => new List<ObterContatoOutput.TelefoneOutput>
        {
            new()
            {
                Numero = f.Person.Phone,
                Tipo = f.PickRandom<TipoTelefone>().ToString(),
                Ddd = f.Random.Short(11, 99)
            }
        });
    }

    public List<ObterContatoOutput> Build(int count)
    {
        return _faker.Generate(count);
    }
}