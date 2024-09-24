// using Contatos.Application.DTOs.Inputs;
// using Contatos.Application.Test.Fixtures;
// using Contatos.Application.UseCases;
// using Contatos.Domain.Entities;
// using Contatos.Domain.Repositories;
// using FluentAssertions;
// using Moq;
// using Moq.AutoMock;
//
// namespace Contatos.Application.Test.UseCases;
//
// [Collection(nameof(ContatoCollection))]
// public class CadastrarContatoUseCaseTest
// {
//     private readonly ContatoFixture _fixture;
//     private readonly AutoMocker _mocker;
//
//     public CadastrarContatoUseCaseTest(ContatoFixture fixture)
//     {
//         _fixture = fixture;
//         _mocker = new AutoMocker();
//     }
//
//     [Fact(DisplayName = "Cadastrar contato com valores válidos deve cadastrar contato com sucesso")]
//     [Trait("Category", "CadastrarContatoUseCase")]
//     public async Task CadastrarContatoUseCase_ContatoValido_DeveCadastrarContatoComSucesso()
//     {
//         // Arrange
//         _mocker.GetMock<IContatoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(() => true);
//         var useCase = _mocker.CreateInstance<CadastrarContatoUseCase>();
//         var input = new NovoContatoInput
//         {
//             Nome = "Fulano",
//             Sobrenome = "Silva",
//             Email = "email@domain.com",
//             Telefones = [_fixture.GerarTelefoneValido()]
//         };
//
//         // Act
//         var result = await useCase.ExecuteAsync(input);
//
//         // Assert
//         result.IsSuccess.Should().BeTrue("o contato deve ser cadastrado");
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.Adicionar(It.IsAny<Contato>()), Times.Once);
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
//     }
//
//     [Fact(DisplayName = "Cadastrar contato com valores inválidos deve retornar erro")]
//     [Trait("Category", "CadastrarContatoUseCase")]
//     public async Task CadastrarContatoUseCase_ContatoInvalido_DeveRetornarErroParaDadosInvalidos()
//     {
//         // Arrange
//         var useCase = _mocker.CreateInstance<CadastrarContatoUseCase>();
//         var input = new NovoContatoInput
//         {
//             Nome = "",
//             Sobrenome = "",
//             Email = "email",
//             Telefones = []
//         };
//
//         // Act
//         var result = await useCase.ExecuteAsync(input);
//
//         // Assert
//         result.IsSuccess.Should().BeFalse("o contato não deve ser cadastrado");
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.Adicionar(It.IsAny<Contato>()), Times.Never);
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
//     }
// }