using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Test.Commons.Builders.Domain.ValueObjects;

namespace Contatos.Domain.Test.ValueObjects;

public class EmailTest
{
    [Theory(DisplayName = "E-mail válido, deve estar válido")]
    [Trait("Category", "Unit Test - Email")]
    [SuppressMessage("Usage", "xUnit1012:Null should only be used for nullable parameters")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("lorem@ipsum.com")]
    [InlineData("user.name+tag+sorting@example.com")]
    [InlineData("email@domain.com")]
    [InlineData("firstname.lastname@domain.com")]
    [InlineData("email@subdomain.domain.com")]
    [InlineData("email@domain.co.jp")]
    [InlineData("firstname+lastname@domain.com")]
    [InlineData("email@123.123.123.123")]
    [InlineData("email@[123.123.123.123]")]
    [InlineData("1234567890@domain.com")]
    public void Email_Valido_DeveEstarValido(string endereco)
    {
        // Arrange
        var email = new EmailBuilder().ComEndereco(endereco).Build();

        // Act
        var result = email.Validar();

        // Assert
        result.IsValid.Should().BeTrue("o e-mail deve estar válido");
    }

    [Theory(DisplayName = "E-mail inválido deve estar inválido")]
    [Trait("Category", "Unit Test - Email")]
    [InlineData("plainaddress")]
    [InlineData("@missingusername.com")]
    [InlineData("username@.com")]
    public void Email_Invalido_DeveEstarInvalido(string endereco)
    {
        // Arrange
        var email = new EmailBuilder().ComEndereco(endereco).Build();

        // Act
        var result = email.Validar();

        // Assert
        result.IsValid.Should().BeFalse("o e-mail deve estar inválido");
    }

    [Fact(DisplayName = "ToString deve retornar o endereço de email")]
    [Trait("Category", "Unit Test - Email")]
    public void Email_ToString_DeveRetornarEnderecoEmail()
    {
        // Arrange
        var extepcted = "lorem@ipsum.com";
        var email = new EmailBuilder().ComEndereco(extepcted).Build();

        // Act & Assert
        email.ToString().Should().Be(extepcted, "o email {0} deve ser igual a {1}", email.Endereco, extepcted);
    }
}