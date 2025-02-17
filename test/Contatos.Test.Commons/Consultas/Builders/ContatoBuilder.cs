using Bogus;
using Contatos.Consulta.Api.Domain.Entities;

namespace Contatos.Test.Commons.Consultas.Builders;

public class ContatoBuilder
{
    private readonly Faker<Contato> _faker;

    public ContatoBuilder()
    {
        var nome = new NomeBuilder().Build();
        var telefones = new TelefoneBuilder().Build(3);
        var email = new EmailBuilder().Build();

        _faker = new Faker<Contato>("pt_BR")
            .CustomInstantiator(f => new Contato
            {
                Nome = nome,
                Telefones = telefones,
                Email = email
            });
    }

    public Contato Build()
    {
        return _faker.Generate();
    }

    public List<Contato> Build(int count)
    {
        return _faker.Generate(count);
    }
}