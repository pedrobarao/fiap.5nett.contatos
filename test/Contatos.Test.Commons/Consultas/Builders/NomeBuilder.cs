using Bogus;
using Contatos.Consulta.Api.Domain.ValueObjects;

namespace Contatos.Test.Commons.Consultas.Builders;

public class NomeBuilder
{
    private readonly Faker<Nome> _faker = new()
    {
        Locale = "pt_BR"
    };

    public NomeBuilder()
    {
        _faker.CustomInstantiator(f => new Nome(
            f.Person.FirstName,
            f.Person.LastName
        ));
    }

    public Nome Build()
    {
        return _faker.Generate();
    }
}