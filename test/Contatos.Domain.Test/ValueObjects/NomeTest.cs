﻿using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Test.Commons.Builders.Domain.ValueObjects;

namespace Contatos.Domain.Test.ValueObjects;

public class NomeTest
{
    [Theory(DisplayName = "Primeiro nome inválido deve falhar")]
    [InlineData(null)]
    [InlineData("")]
    [Trait("Category", "Unit Test - Nome")]
    [SuppressMessage("Usage", "xUnit1012:Null should only be used for nullable parameters")]
    public void Nome_PrimeiroNomeInvalido_DeveEstarInvalido(string primeiroNome)
    {
        // Arrange
        var nome = new NomeBuilder()
            .ComPrimeiroNome(primeiroNome)
            .Build();

        // Act
        var result = nome.Validar();

        // Assert
        result.IsValid.Should().BeFalse("o nome {0} deve estar inválido", nome);
    }

    [Fact(DisplayName = "Sobrenome nulo ou vazio deve passar")]
    [Trait("Category", "Unit Test - Nome")]
    public void Nome_SobrenomeNuloOuVazio_DeveEstarValido()
    {
        // Arrange
        var nomeSobrenomeNulo = new NomeBuilder()
            .ComSobrenomeNulo()
            .Build();

        var nomeSobrenomeVazio = new NomeBuilder()
            .ComSobrenomeVazio()
            .Build();

        // Act
        var resultNomeSobrenomeNulo = nomeSobrenomeNulo.Validar();
        var resultNomeSobrenomeVazio = nomeSobrenomeVazio.Validar();

        // Assert
        resultNomeSobrenomeNulo.IsValid.Should().BeTrue("o nome {0} deve estar válido", nomeSobrenomeNulo);
        resultNomeSobrenomeVazio.IsValid.Should().BeTrue("o nome {0} deve estar válido", nomeSobrenomeVazio);
    }

    [Fact(DisplayName = "ToString deve retornar o nome completo")]
    [Trait("Category", "Unit Test - Nome")]
    public void Nome_ToString_DeveRetornarONomeESobrenome()
    {
        // Arrange
        var nome = new NomeBuilder().Build();
        var expected = $"{nome.PrimeiroNome} {nome.Sobrenome}";

        // Act & Assert
        nome.ToString().Should().Be(expected, "o nome {0} deve ser igual a {1}", nome, expected);
    }
}