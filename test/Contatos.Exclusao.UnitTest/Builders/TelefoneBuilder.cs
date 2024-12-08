using Bogus;

namespace Contatos.Exclusao.UnitTest.Builders;

public class TelefoneBuilder
{
    private readonly Faker<Telefone> _faker = new()
    {
        Locale = "pt_BR"
    };

    public TelefoneBuilder()
    {
        _faker.CustomInstantiator(f =>
        {
            var ddd = f.Random.Short(11, 99);
            var numero = f.Random.Replace("9########");
            var tipo = f.PickRandom<TipoTelefone>();

            if (tipo != TipoTelefone.Celular) numero = numero.Remove(0, 1);

            return new Telefone(ddd, numero, tipo);
        });
    }

    public TelefoneBuilder ComDdd(short ddd)
    {
        _faker.RuleFor(p => p.Ddd, ddd);
        return this;
    }

    public TelefoneBuilder ComNumero(string numero)
    {
        _faker.RuleFor(p => p.Numero, numero);
        return this;
    }

    public TelefoneBuilder ComTipo(TipoTelefone tipo)
    {
        _faker.RuleFor(p => p.Tipo, tipo);
        return this;
    }

    public Telefone Build()
    {
        return _faker.Generate();
    }

    public List<Telefone> Build(int count)
    {
        return _faker.Generate(count);
    }
}