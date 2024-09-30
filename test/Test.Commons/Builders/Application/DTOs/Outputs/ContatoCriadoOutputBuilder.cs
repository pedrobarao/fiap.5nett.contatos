using Bogus;
using Contatos.Application.DTOs.Outputs;
using Test.Commons.Builders.Domain.ValueObjects;

namespace Test.Commons.Builders.Application.DTOs.Outputs;

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