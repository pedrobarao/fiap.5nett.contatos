using Bogus;

namespace Contatos.Cadastro.UnitTest.Builders;

public class ContatoCriadoOutputBuilder
{
    private readonly Faker<ContatoCriadoOutput> _faker = new()
    {
        Locale = "pt_BR"
    };

    public ContatoCriadoOutputBuilder()
    {
        _faker.RuleFor(c => c.Id, f => f.Random.Guid());
    }

    public ContatoCriadoOutput Build()
    {
        return _faker.Generate();
    }
}