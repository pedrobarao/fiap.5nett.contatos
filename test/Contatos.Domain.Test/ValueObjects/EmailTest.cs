// using Contatos.Domain.Test.Fixtures;
// using FluentAssertions;
//
// namespace Contatos.Domain.Test.ValueObjects;
//
// [Collection(nameof(ContatoCollection))]
// public class EmailTest
// {
//     private readonly ContatoFixture _fixture;
//
//     public EmailTest(ContatoFixture fixture)
//     {
//         _fixture = fixture;
//     }
//
//     [Theory(DisplayName = "E-mail válido")]
//     [Trait("Category", "Email")]
//     [InlineData("lorem@ipsum.com")]
//     [InlineData("user.name+tag+sorting@example.com")]
//     [InlineData("email@domain.com")]
//     [InlineData("firstname.lastname@domain.com")]
//     [InlineData("email@subdomain.domain.com")]
//     [InlineData("email@domain.co.jp")]
//     [InlineData("firstname+lastname@domain.com")]
//     [InlineData("email@123.123.123.123")]
//     [InlineData("email@[123.123.123.123]")]
//     [InlineData("1234567890@domain.com")]
//     public void Email_EmailValido_DeveEstarValido(string endereco)
//     {
//         // Arrange
//         var email = _fixture.GerarEmail(endereco);
//
//         // Act
//         var result = email.Validar();
//
//         // Assert
//         result.IsValid.Should().BeTrue("o e-mail deve estar válido");
//     }
//
//     [Theory(DisplayName = "E-mail inválido")]
//     [Trait("Category", "Email")]
//     [InlineData("plainaddress")]
//     [InlineData("@missingusername.com")]
//     [InlineData("username@.com")]
//     public void Email_Invalido_DeveEstarInvalido(string endereco)
//     {
//         // Arrange
//         var email = _fixture.GerarEmail(endereco);
//
//         // Act
//         var result = email.Validar();
//
//         // Assert
//         result.IsValid.Should().BeFalse("o e-mail deve estar inválido");
//     }
//
//     [Fact(DisplayName = "E-mail nulo ou vazio deve estar válido")]
//     [Trait("Category", "Email")]
//     public void Email_NuloOuVazio_DeveEstarValido()
//     {
//         // Arrange
//         var emailNulo = _fixture.GerarEmailNulo();
//         var emailVazio = _fixture.GerarEmailVazio();
//
//         // Act
//         var resultNulo = emailNulo.Validar();
//         var resultVazio = emailVazio.Validar();
//
//         // Assert
//         resultNulo.IsValid.Should().BeTrue("o e-mail nulo deve estar válido");
//         resultVazio.IsValid.Should().BeTrue("o e-mail vazio deve estar válido");
//     }
// }