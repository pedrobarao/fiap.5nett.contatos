﻿using Bogus;

namespace Contatos.Consulta.UnitTest.Builders;

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