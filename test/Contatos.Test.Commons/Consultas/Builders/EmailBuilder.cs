using Bogus;
using Contatos.Consulta.Api.Domain.ValueObjects;

namespace Contatos.Test.Commons.Consultas.Builders;

public class EmailBuilder
{
    private readonly Faker<Email> _faker = new()
    {
        Locale = "pt_BR"
    };

    public EmailBuilder()
    {
        _faker.CustomInstantiator(f => new Email(f.Internet.Email()));
    }

    public Email Build()
    {
        return _faker.Generate();
    }
}