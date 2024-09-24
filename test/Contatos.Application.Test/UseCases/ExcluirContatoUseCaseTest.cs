// using Commons.Domain.DomainObjects;
// using Contatos.Application.Test.Fixtures;
// using Contatos.Application.UseCases;
// using Contatos.Domain.Entities;
// using Contatos.Domain.Repositories;
// using Moq;
// using Moq.AutoMock;
//
// namespace Contatos.Application.Test.UseCases;
//
// [Collection(nameof(ContatoCollection))]
// public class ExcluirContatoUseCaseTest
// {
//     private readonly ContatoFixture _fixture;
//     private readonly AutoMocker _mocker;
//
//     public ExcluirContatoUseCaseTest(ContatoFixture fixture)
//     {
//         _fixture = fixture;
//         _mocker = new AutoMocker();
//     }
//
//     [Fact(DisplayName = "Excluir contato válido deve excluir contato com sucesso")]
//     [Trait("Category", "ExcluirContatoUseCase")]
//     public async Task ExcluirContatoUseCase_ContatoValido_DeveExcluirContatoComSucesso()
//     {
//         // Arrange
//         var contato = _fixture.GerarContatoValido();
//         _mocker.GetMock<IContatoRepository>().Setup(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()))
//             .ReturnsAsync(contato);
//         _mocker.GetMock<IContatoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(() => true);
//         var useCase = _mocker.CreateInstance<ExcluirContatoUseCase>();
//
//         // Act
//         await useCase.ExecuteAsync(contato.Id);
//
//         // Assert
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.Excluir(It.IsAny<Contato>()), Times.Once);
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
//     }
//
//     [Fact(DisplayName = "Excluir contato inválido deve retornar erro")]
//     [Trait("Category", "ExcluirContatoUseCase")]
//     public async Task ExcluirContatoUseCase_ContatoInvalido_DeveRetornarErro()
//     {
//         // Arrange
//         _mocker.GetMock<IContatoRepository>().Setup(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()))
//             .ReturnsAsync(() => null);
//         var useCase = _mocker.CreateInstance<ExcluirContatoUseCase>();
//
//         // Act & Assert
//         await Assert.ThrowsAsync<DomainException>(() => useCase.ExecuteAsync(It.IsAny<Guid>()));
//     }
// }