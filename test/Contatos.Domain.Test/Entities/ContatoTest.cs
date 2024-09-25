using FluentAssertions;
using Test.Commons.Builders.Entities;
using Test.Commons.Builders.ValueObjects;

namespace Contatos.Domain.Test.Entities;

public class ContatoTest
{
    [Fact(DisplayName = "Contato com valores válidos deve estar válido")]
    [Trait("Category", "Contato")]
    public void Contato_Valido_DeveEstarValido()
    {
        // Arrange
        var contato = new ContatoBuilder().Build();

        // Act
        var result = contato.Validar();

        // Assert
        result.IsValid.Should().BeTrue("o contato deve estar válido");
    }

    [Fact(DisplayName = "Contato com nome inválido deve estar inválido")]
    [Trait("Category", "Contato")]
    public void Contato_ComNomeInvalido_DeveEstarInvalido()
    {
        // Arrange
        var contato = new ContatoBuilder()
            .ComNomeInvalido()
            .Build();

        // Act
        var result = contato.Validar();

        // Assert
        result.IsValid.Should().BeFalse("o contato deve estar inválido");
    }

    [Fact(DisplayName = "Contato com telefone inválido deve estar inválido")]
    [Trait("Category", "Contato")]
    public void Contato_ComTelefoneInvalido_DeveEstarInvalido()
    {
        // Arrange
        var contato = new ContatoBuilder()
            .ComTelefoneInvalido()
            .Build();

        // Act
        var result = contato.Validar();

        // Assert
        result.IsValid.Should().BeFalse("o contato deve estar inválido");
    }

    [Fact(DisplayName = "Contato com email inválido deve estar inválido")]
    [Trait("Category", "Contato")]
    public void Contato_ComEmailInvalido_DeveEstarInvalido()
    {
        // Arrange
        var contato = new ContatoBuilder()
            .ComEmailInvalido()
            .Build();

        // Act
        var result = contato.Validar();

        // Assert
        result.IsValid.Should().BeFalse("o contato deve estar inválido");
    }

    [Fact(DisplayName = "Atualizar nome do contato deve atualizar o nome")]
    [Trait("Category", "Contato")]
    public void Contato_AtualizarNome_NomeDeveSerAtualizado()
    {
        // Arrange
        var expected = new NomeBuilder().Build();
        var contato = new ContatoBuilder().Build();

        // Act
        contato.AtualizarNome(expected);

        // Assert
        contato.Nome.Should().Be(expected, "o nome do contato deve ser igual a {0}", expected);
    }
    
    [Fact(DisplayName = "Atualizar telefones do contato deve atualizar a lista de telefones")]
    [Trait("Category", "Contato")]
    public void Contato_AtualizarTelefones_TelefonesDevemSerAtualizados()
    {
        // Arrange
        var expected = new TelefoneBuilder().Build(3);
        var contato = new ContatoBuilder().Build();

        // Act
        contato.AtualizarTelefones(expected);

        // Assert
        contato.Telefones.Should().AllSatisfy(e => expected.Contains(e), "todos os telefones devem estar na lista de telefones");
    }
    
    [Fact(DisplayName = "Atualizar e-mail do contato deve atualizar o endereço de e-mail")]
    [Trait("Category", "Contato")]
    public void Contato_AtualizarEmail_EmailDeveSerAtualizado()
    {
        // Arrange
        var expected = new EmailBuilder().Build();
        var contato = new ContatoBuilder().Build();

        // Act
        contato.AtualizarEmail(expected);

        // Assert
        contato.Email.Should().Be(expected, "o e-mail do contato deve ser igual a {0}", expected);
    }
}