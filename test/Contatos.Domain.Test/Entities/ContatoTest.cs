// using Contatos.Domain.Test.Fixtures;
//
// namespace Contatos.Domain.Test.Entities;
//
// [Collection(nameof(ContatoCollection))]
// public class ContatoTest
// {
//     [Fact(DisplayName = "Contato com valores válidos deve estar válido")]
//     [Trait("Category", "Contato")]
//     public void Contato_ValoresValidos_DeveEstarValido()
//     {
//         // Arrange
//         var contato = _fixture.GerarContatoValido();
//
//         // Act
//         var result = contato.Validar();
//
//         // Assert
//         result.IsValid.Should().BeTrue("o contato deve estar válido");
//     }
//
//     [Fact(DisplayName = "Contato com nome inválido deve estar inválido")]
//     [Trait("Category", "Contato")]
//     public void Contato_ComNomeInvalido_DeveEstarInvalido()
//     {
//         // Arrange
//         var contato = _fixture.GerarContatoComNomeInvalido();
//
//         // Act
//         var result = contato.Validar();
//
//         // Assert
//         result.IsValid.Should().BeFalse("o contato deve estar inválido");
//     }
//
//     [Fact(DisplayName = "Contato com e-mail inválido deve estar inválido")]
//     [Trait("Category", "Contato")]
//     public void Contato_EmailInvalido_DeveEstarInvalido()
//     {
//         // Arrange
//         var contato = _fixture.GerarContatoComEmailInvalido();
//
//         // Act
//         var result = contato.Validar();
//
//         // Assert
//         result.IsValid.Should().BeFalse("o contato deve estar inválido");
//     }
//
//     [Fact(DisplayName = "Contato com telefone inválido deve estar inválido")]
//     [Trait("Category", "Contato")]
//     public void Contato_TelefoneInvalido_DeveEstarInvalido()
//     {
//         // Arrange
//         var contato = _fixture.GerarContatoComTelefoneInvalido();
//
//         // Act
//         var result = contato.Validar();
//
//         // Assert
//         result.IsValid.Should().BeFalse("o contato deve estar inválido");
//     }
// }