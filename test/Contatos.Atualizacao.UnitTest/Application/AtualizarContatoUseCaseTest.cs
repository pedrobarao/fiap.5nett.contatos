// using Commons.Domain.DomainObjects;
// using Contatos.Atualizacao.Api.Application.UseCases;
// using Contatos.Atualizacao.Api.Domain;
// using Contatos.Atualizacao.UnitTest.Builders;
// using Contatos.SharedKernel.Entities;
// using FluentAssertions;
// using Moq;
// using Moq.AutoMock;
//
// namespace Contatos.Atualizacao.UnitTest.Application;
//
// public class AtualizarContatoUseCaseTest
// {
//     private readonly AutoMocker _mocker;
//     private readonly IAtualizarContatoUseCase _useCase;
//
//     public AtualizarContatoUseCaseTest()
//     {
//         _mocker = new AutoMocker();
//         _useCase = _mocker.CreateInstance<AtualizarContatoUseCase>();
//         _mocker.GetMock<IContatoRepository>()
//             .Setup(r => r.UnitOfWork.Commit())
//             .ReturnsAsync(() => true);
//     }
//
//     [Fact(DisplayName = "Atualizar contato com valores válidos deve atualizar com sucesso")]
//     [Trait("Category", "Unit Test - AtualizarContatoUseCase")]
//     public async Task ExecuteAsync_ContatoValido_DeveAtualizarContatoComSucesso()
//     {
//         // Arrange
//         var contato = new ContatoBuilder().Build();
//
//         var input = new AtualizarContatoInputBuilder()
//             .ComIdContato(contato.Id)
//             .Build();
//
//         _mocker.GetMock<IContatoRepository>()
//             .Setup(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()))
//             .ReturnsAsync(contato);
//
//         // Act
//         var result = await _useCase.ExecuteAsync(input);
//
//         // Assert
//         result.IsSuccess.Should().BeTrue("o contato deve ser atualizado");
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()), Times.Once);
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.Atualizar(It.IsAny<Contato>()), Times.Once);
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
//     }
//
//     [Fact(DisplayName = "Atualizar contato que não existe deve retornar exceção de domínio")]
//     [Trait("Category", "Unit Test - AtualizarContatoUseCase")]
//     public async Task ExecuteAsync_COntatoNaoExiste_DeveRetornarErro()
//     {
//         // Arrange
//         var input = new AtualizarContatoInputBuilder()
//             .Build();
//
//         // Act
//         var act = async () => await _useCase.ExecuteAsync(input);
//
//         // Assert
//         await act.Should().ThrowAsync<DomainException>()
//             .WithMessage("Contato inválido.", "deve retornar uma exceção de domínio");
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()), Times.Once);
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.Atualizar(It.IsAny<Contato>()), Times.Never);
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
//     }
//
//     [Fact(DisplayName = "Atualizar contato com nome inválido deve retornar erro")]
//     [Trait("Category", "Unit Test - AtualizarContatoUseCase")]
//     public async Task ExecuteAsync_ContatoComNomeInvalido_DeveRetornarErro()
//     {
//         // Arrange
//         var contato = new ContatoBuilder().Build();
//
//         var input = new AtualizarContatoInputBuilder()
//             .ComIdContato(contato.Id)
//             .ComNomeInvalido()
//             .Build();
//
//         _mocker.GetMock<IContatoRepository>()
//             .Setup(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()))
//             .ReturnsAsync(contato);
//
//         // Act
//         var result = await _useCase.ExecuteAsync(input);
//
//         // Assert
//         result.IsSuccess.Should().BeFalse("deve retornar erro");
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()), Times.Once);
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.Atualizar(It.IsAny<Contato>()), Times.Never);
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
//     }
//
//     [Fact(DisplayName = "Atualizar contato com email inválido deve retornar erro")]
//     [Trait("Category", "Unit Test - AtualizarContatoUseCase")]
//     public async Task ExecuteAsync_ContatoComEmailInvalido_DeveRetornarErro()
//     {
//         // Arrange
//         var contato = new ContatoBuilder().Build();
//
//         var input = new AtualizarContatoInputBuilder()
//             .ComIdContato(contato.Id)
//             .ComEmailInvalido()
//             .Build();
//
//         _mocker.GetMock<IContatoRepository>()
//             .Setup(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()))
//             .ReturnsAsync(contato);
//
//         // Act
//         var result = await _useCase.ExecuteAsync(input);
//
//         // Assert
//         result.IsSuccess.Should().BeFalse("deve retornar erro");
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()), Times.Once);
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.Atualizar(It.IsAny<Contato>()), Times.Never);
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
//     }
//
//     [Fact(DisplayName = "Atualizar contato com telefone inválido deve retornar erro")]
//     [Trait("Category", "Unit Test - AtualizarContatoUseCase")]
//     public async Task ExecuteAsync_ContatoComTelefoneInvalido_DeveRetornarErro()
//     {
//         // Arrange
//         var contato = new ContatoBuilder().Build();
//
//         var input = new AtualizarContatoInputBuilder()
//             .ComIdContato(contato.Id)
//             .ComTelefoneInvalido()
//             .Build();
//
//         _mocker.GetMock<IContatoRepository>()
//             .Setup(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()))
//             .ReturnsAsync(contato);
//
//         // Act
//         var result = await _useCase.ExecuteAsync(input);
//
//         // Assert
//         result.IsSuccess.Should().BeFalse("deve retornar erro");
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()), Times.Once);
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.Atualizar(It.IsAny<Contato>()), Times.Never);
//         _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
//     }
// }

