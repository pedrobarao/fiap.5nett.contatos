using Bogus;
using Contatos.Consulta.Api.Domain.ValueObjects;

namespace Contatos.Test.Commons.Consultas.Builders;

public class TelefoneBuilder
{
    private readonly Faker<Telefone> _faker = new()
    {
        Locale = "pt_BR"
    };

    public TelefoneBuilder()
    {
        _faker.CustomInstantiator(f => { return new Telefone(32, "32129874", "Tipo"); });
    }

    public List<Telefone> Build(int count)
    {
        return _faker.Generate(count);
    }
}