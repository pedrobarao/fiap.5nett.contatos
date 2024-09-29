using Bogus;
using Contatos.Application.DTOs.Outputs;
using Contatos.Domain.ValueObjects;
using Test.Commons.Builders.Domain.ValueObjects;

namespace Test.Commons.Builders.Application.DTOs.Outputs;

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

        var telefones = new TelefoneBuilder().Build(3);
        var telefonesOutput = new List<ObterContatoOutput.TelefoneOutput>();
        foreach (var telefone in telefones)
        {
            telefonesOutput.Add(new ObterContatoOutput.TelefoneOutput
            {
                Numero = telefone.Numero,
                Tipo = telefone.Tipo.ToString(),
                Ddd = telefone.Ddd
            });
        }
        
        _faker.RuleFor(c => c.Telefones, f => telefonesOutput);
    }

    public ObterContatoOutput Build()
    {
        return _faker.Generate();
    }
    
    public List<ObterContatoOutput> Build(int count)
    {
        return _faker.Generate(count);
    }
}