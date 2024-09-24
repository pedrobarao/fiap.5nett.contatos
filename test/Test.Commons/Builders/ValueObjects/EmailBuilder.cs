using Bogus;
using Contatos.Domain.ValueObjects;

namespace Test.Commons.Builders.ValueObjects;

public class EmailBuilder
{
    private readonly Faker<Email> _faker = new()
    {
        Locale = "pt_BR"
    };

    public EmailBuilder()
    {
        _faker.RuleFor(x => x.Endereco, f => f.Internet.Email());
    }
    
    public EmailBuilder ComEndereco(string email)
    {
        _faker.RuleFor(x => x.Endereco, email);
        return this;
    }
    
    public Email Build()
    {
        return _faker.Generate();
    }
}