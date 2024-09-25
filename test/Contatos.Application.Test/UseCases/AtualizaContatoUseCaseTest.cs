// using Bogus;
// using Commons.Domain.DomainObjects;
// using Contatos.Application.DTOs.Inputs;
// using Contatos.Application.Test.Fixtures;
// using Contatos.Application.UseCases;
// using Contatos.Domain.Repositories;
// using FluentAssertions;
// using Moq;
// using Moq.AutoMock;
//
// namespace Contatos.Application.Test.UseCases;
//
// [Collection(nameof(ContatoCollection))]
// public class AtualizaContatoUseCaseTest
// {
//     private readonly ContatoFixture _fixture;
//     private readonly AutoMocker _mocker;
//
//     public AtualizaContatoUseCaseTest(ContatoFixture fixture)
//     {
//         _fixture = fixture;
//         _mocker = new AutoMocker();
//     }
//
//     [Fact(DisplayName = "Atualizar contato com valores válidos deve atualizar contato com sucesso")]
//     [Trait("Category", "AtualizarContatoUseCase")]
//     public async Task AtualizarContatoUseCase_ContatoValido_DeveAtualizarContatoComSucesso()
//     {
//         // Arrange
//         var contato = _fixture.GerarContatoValido();
//         _mocker.GetMock<IContatoRepository>().Setup(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()))
//             .ReturnsAsync(contato);
//         _mocker.GetMock<IContatoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(() => true);
//         var useCase = _mocker.CreateInstance<AtualizarContatoUseCase>();
//         var faker = new Faker("pt_BR");
//         var input = new AtualizarContatoInput
//         {
//             Id = contato.Id,
//             Nome = faker.Person.FirstName,
//             Sobrenome = faker.Person.LastName,
//             Email = faker.Person.Email,
//             Telefones = [_fixture.GerarTelefoneValido()]
//         };
//
//         // Act
//         var result = await useCase.ExecuteAsync(input);
//
//         // Assert
//         result.IsSuccess.Should().BeTrue("o contato deve ser atualizado");
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.Atualizar(contato), Times.Once);
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
//     }
//
//     [Fact(DisplayName = "Atualizar contato e contato não existe deve retornar erro")]
//     [Trait("Category", "AtualizarContatoUseCase")]
//     public async Task AtualizarContatoUseCase_ContatoNaoExiste_DeveRetornarErro()
//     {
//         // Arrange
//         _mocker.GetMock<IContatoRepository>().Setup(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()))
//             .ReturnsAsync(() => null);
//         var useCase = _mocker.CreateInstance<AtualizarContatoUseCase>();
//         var faker = new Faker("pt_BR");
//         var input = new AtualizarContatoInput
//         {
//             Id = Guid.NewGuid(),
//             Nome = faker.Person.FirstName,
//             Sobrenome = faker.Person.LastName,
//             Email = faker.Person.Email,
//             Telefones = [_fixture.GerarTelefoneValido()]
//         };
//
//         // Act & Assert
//         await Assert.ThrowsAsync<DomainException>(() => useCase.ExecuteAsync(input));
//     }
//
//     [Fact(DisplayName = "Atualizar contato com dados inválidos deve retornar erro")]
//     [Trait("Category", "AtualizarContatoUseCase")]
//     public async Task AtualizarContatoUseCase_ContatoInvalido_DeveRetornarErro()
//     {
//         // Arrange
//         var contatoInvalido = _fixture.GerarContatoInvalido();
//         var contatoValido = _fixture.GerarContatoValido();
//         _mocker.GetMock<IContatoRepository>().Setup(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()))
//             .ReturnsAsync(contatoValido);
//         var useCase = _mocker.CreateInstance<AtualizarContatoUseCase>();
//         var input = new AtualizarContatoInput
//         {
//             Id = contatoValido.Id,
//             Nome = contatoInvalido.Nome.PrimeiroNome,
//             Sobrenome = contatoInvalido.Nome.Sobrenome,
//             Email = contatoInvalido.Email!.Endereco,
//             Telefones = contatoInvalido.Telefones.ToList()
//         };
//
//         // Act
//         var result = await useCase.ExecuteAsync(input);
//
//         // Assert
//         // Assert
//         result.IsSuccess.Should().BeFalse("o contato deve não deve ser atualizado");
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.Atualizar(contatoInvalido), Times.Never);
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
//     }
// }

