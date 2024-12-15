using System.Diagnostics.CodeAnalysis;
using Contatos.Cadastro.Api.Domain.ValueObjects;
using Contatos.Test.Commons.Cadastros.Builders;
using FluentAssertions;

namespace Contatos.Cadastro.UnitTest.Domain.ValueObjects;

public class TelefoneTest
{
    [Theory(DisplayName = "Telefone com DDD inválido deve estar inválido")]
    [InlineData(null)]
    [InlineData((short)0)]
    [InlineData((short)1)]
    [InlineData((short)2)]
    [InlineData((short)3)]
    [InlineData((short)4)]
    [InlineData((short)5)]
    [InlineData((short)6)]
    [InlineData((short)7)]
    [InlineData((short)8)]
    [InlineData((short)9)]
    [InlineData((short)10)]
    [Trait("Category", "Unit Test - Telefone (Cadastros)")]
    [SuppressMessage("Usage", "xUnit1012:Null should not be used for value type parameters")]
    public void Telefone_DddInvalido_DeveEstarInvalido(short ddd)
    {
        // Arrange
        var telefone = new TelefoneBuilder().ComDdd(ddd).Build();

        // Act
        var result = telefone.Validar();

        // Assert
        result.IsValid.Should().BeFalse("o telefone {0} deve estar inválido.", telefone);
    }

    [Theory(DisplayName = "Telefone com DDD válido deve estar válido")]
    [InlineData((short)11)]
    [InlineData((short)21)]
    [InlineData((short)31)]
    [InlineData((short)41)]
    [InlineData((short)51)]
    [InlineData((short)61)]
    [InlineData((short)71)]
    [InlineData((short)81)]
    [InlineData((short)91)]
    [Trait("Category", "Unit Test - Telefone (Cadastros)")]
    public void Telefone_DddValido_DeveEstarValido(short ddd)
    {
        // Arrange
        var telefone = new TelefoneBuilder().ComDdd(ddd).Build();

        // Act
        var result = telefone.Validar();

        // Assert
        result.IsValid.Should().BeTrue("o telefone {0} deve estar válido.", telefone);
    }

    [Theory(DisplayName = "Telefone com número residencial e comercial inválido, deve estar inválido")]
    [SuppressMessage("Usage", "xUnit1012:Null should only be used for nullable parameters")]
    [InlineData(null)]
    [InlineData("3212")]
    [InlineData("1011")]
    [InlineData("3212101")]
    [InlineData("932121011")]
    [InlineData("9321210111")]
    [InlineData("93212101111")]
    [Trait("Category", "Unit Test - Telefone (Cadastros)")]
    public void Telefone_NumeroResidencialEComercialInvalido_DeveEstarInvalido(string numero)
    {
        // Arrange
        var telefoneResidencial = new TelefoneBuilder()
            .ComTipo(TipoTelefone.Residencial)
            .ComNumero(numero)
            .Build();

        var telefoneComercial = new TelefoneBuilder()
            .ComTipo(TipoTelefone.Comercial)
            .ComNumero(numero)
            .Build();

        // Act
        var resultTelefoneResidencial = telefoneResidencial.Validar();
        var resultTelefoneComercial = telefoneComercial.Validar();

        // Assert
        resultTelefoneResidencial.IsValid.Should()
            .BeFalse("o telefone residencial {0} deve estar inválido.", telefoneResidencial);
        resultTelefoneComercial.IsValid.Should()
            .BeFalse("o telefone comercial {0} deve estar inválido.", resultTelefoneComercial);
    }

    [Theory(DisplayName = "Telefone com número residencial e comercial inválido, deve estar inválido")]
    [InlineData("32125867")]
    [InlineData("21258674")]
    [InlineData("12586732")]
    [Trait("Category", "Unit Test - Telefone (Cadastros)")]
    public void Telefone_NumeroResidencialEComercialValido_DeveEstarValido(string numero)
    {
        // Arrange
        var telefoneResidencial = new TelefoneBuilder()
            .ComTipo(TipoTelefone.Residencial)
            .ComNumero(numero)
            .Build();

        var telefoneComercial = new TelefoneBuilder()
            .ComTipo(TipoTelefone.Comercial)
            .ComNumero(numero)
            .Build();

        // Act
        var resultTelefoneResidencial = telefoneResidencial.Validar();
        var resultTelefoneComercial = telefoneComercial.Validar();

        // Assert
        resultTelefoneResidencial.IsValid.Should()
            .BeTrue("o telefone residencial {0} deve estar válido.", telefoneResidencial);
        resultTelefoneComercial.IsValid.Should()
            .BeTrue("o telefone comercial {0} deve estar válido.", resultTelefoneComercial);
    }

    [Fact(DisplayName = "ToString deve retornar o número do telefone com DDD")]
    [Trait("Category", "Unit Test - Telefone (Cadastros)")]
    public void Telefone_ToString_DeveRetornarONumeroComDdd()
    {
        // Arrange
        short ddd = 21;
        const string numero = "987546921";
        var expected = $"{ddd}{numero}";

        var telefone = new TelefoneBuilder()
            .ComDdd(ddd)
            .ComNumero(numero)
            .ComTipo(TipoTelefone.Celular)
            .Build();

        // Act & Assert
        telefone.ToString().Should().Be($"{ddd}{numero}", "o telefone {0} deve ser igual a {1}", telefone, expected);
    }
}