using Commons.Domain.DomainObjects;
using Contatos.Cadastro.Api.Application.Commands.Atualizar;
using Contatos.Cadastro.Api.Domain.Entities;
using Contatos.Cadastro.Api.Domain.Repositories;
using Contatos.Test.Commons.Cadastros.Builders;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace Contatos.Cadastro.UnitTest.Application.Commands;

public class AtualizarContatoCommandHandlerTest
{
    private readonly AtualizarContatoCommandHandler _commandHandler;
    private readonly AutoMocker _mocker;

    public AtualizarContatoCommandHandlerTest()
    {
        _mocker = new AutoMocker();
        _commandHandler = _mocker.CreateInstance<AtualizarContatoCommandHandler>();
        _mocker.GetMock<IContatoRepository>()
            .Setup(r => r.UnitOfWork.Commit())
            .ReturnsAsync(() => true);
    }

    [Fact(DisplayName = "Atualizar contato com valores válidos deve atualizar com sucesso")]
    [Trait("Category", "Unit Test - AtualizarContatoCommandHandler (Cadastros)")]
    public async Task Handle_ContatoValido_DeveAtualizarContatoComSucesso()
    {
        // Arrange
        var contato = new ContatoBuilder().Build();

        var command = new AtualizarContatoCommandBuilder()
            .ComIdContato(contato.Id)
            .Build();

        _mocker.GetMock<IContatoRepository>()
            .Setup(r => r.ObterContatoPorId(It.IsAny<Guid>()))
            .ReturnsAsync(contato);

        // Act
        var result = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue("o contato deve ser atualizado");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.ObterContatoPorId(It.IsAny<Guid>()), Times.Once);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Atualizar(It.IsAny<Contato>()), Times.Once);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
    }

    [Fact(DisplayName = "Atualizar contato que não existe deve retornar exceção de domínio")]
    [Trait("Category", "Unit Test - AtualizarContatoCommandHandler (Cadastros)")]
    public async Task Handle_COntatoNaoExiste_DeveRetornarErro()
    {
        // Arrange
        var command = new AtualizarContatoCommandBuilder()
            .Build();

        // Act
        var act = async () => await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<DomainException>()
            .WithMessage("Contato inválido.", "deve retornar uma exceção de domínio");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.ObterContatoPorId(It.IsAny<Guid>()), Times.Once);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Atualizar(It.IsAny<Contato>()), Times.Never);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }

    [Fact(DisplayName = "Atualizar contato com nome inválido deve retornar erro")]
    [Trait("Category", "Unit Test - AtualizarContatoCommandHandler (Cadastros)")]
    public async Task Handle_ContatoComNomeInvalido_DeveRetornarErro()
    {
        // Arrange
        var contato = new ContatoBuilder().Build();

        var command = new AtualizarContatoCommandBuilder()
            .ComIdContato(contato.Id)
            .ComNomeInvalido()
            .Build();

        _mocker.GetMock<IContatoRepository>()
            .Setup(r => r.ObterContatoPorId(It.IsAny<Guid>()))
            .ReturnsAsync(contato);

        // Act
        var result = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse("deve retornar erro");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.ObterContatoPorId(It.IsAny<Guid>()), Times.Once);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Atualizar(It.IsAny<Contato>()), Times.Never);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }

    [Fact(DisplayName = "Atualizar contato com email inválido deve retornar erro")]
    [Trait("Category", "Unit Test - AtualizarContatoCommandHandler (Cadastros)")]
    public async Task Handle_ContatoComEmailInvalido_DeveRetornarErro()
    {
        // Arrange
        var contato = new ContatoBuilder().Build();

        var command = new AtualizarContatoCommandBuilder()
            .ComIdContato(contato.Id)
            .ComEmailInvalido()
            .Build();

        _mocker.GetMock<IContatoRepository>()
            .Setup(r => r.ObterContatoPorId(It.IsAny<Guid>()))
            .ReturnsAsync(contato);

        // Act
        var result = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse("deve retornar erro");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.ObterContatoPorId(It.IsAny<Guid>()), Times.Once);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Atualizar(It.IsAny<Contato>()), Times.Never);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }

    [Fact(DisplayName = "Atualizar contato com telefone inválido deve retornar erro")]
    [Trait("Category", "Unit Test - AtualizarContatoCommandHandler (Cadastros)")]
    public async Task Handle_ContatoComTelefoneInvalido_DeveRetornarErro()
    {
        // Arrange
        var contato = new ContatoBuilder().Build();

        var command = new AtualizarContatoCommandBuilder()
            .ComIdContato(contato.Id)
            .ComTelefoneInvalido()
            .Build();

        _mocker.GetMock<IContatoRepository>()
            .Setup(r => r.ObterContatoPorId(It.IsAny<Guid>()))
            .ReturnsAsync(contato);

        // Act
        var result = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse("deve retornar erro");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.ObterContatoPorId(It.IsAny<Guid>()), Times.Once);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Atualizar(It.IsAny<Contato>()), Times.Never);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }
}