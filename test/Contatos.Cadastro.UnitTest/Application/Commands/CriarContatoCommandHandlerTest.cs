using Contatos.Cadastro.Api.Application.Commands.Criar;
using Contatos.Cadastro.Api.Domain.Entities;
using Contatos.Cadastro.Api.Domain.Repositories;
using Contatos.Test.Commons.Cadastros.Builders;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace Contatos.Cadastro.UnitTest.Application.Commands;

public class CriarContatoCommandHandlerTest
{
    private readonly CriarContatoCommandHandler _commandHandler;
    private readonly AutoMocker _mocker;

    public CriarContatoCommandHandlerTest()
    {
        _mocker = new AutoMocker();
        _commandHandler = _mocker.CreateInstance<CriarContatoCommandHandler>();
        _mocker.GetMock<IContatoRepository>()
            .Setup(r => r.UnitOfWork.Commit())
            .ReturnsAsync(() => true);
    }

    [Fact(DisplayName = "Cadastrar contato com valores válidos deve criar o contato com sucesso")]
    [Trait("Category", "Unit Test - CriarContatoCommandHandler (Cadastros)")]
    public async Task ExecuteAsync_ContatoValido_DeveCriarContatoComSucesso()
    {
        // Arrange
        var command = new CriarContatoCommandBuilder().Build();

        // Act
        var result = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue("o contato deve ser criado");
        result.Value!.Should().NotBeEmpty("o id do contato deve ser gerado");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Adicionar(It.IsAny<Contato>()), Times.Once);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
    }

    [Fact(DisplayName = "Criar contato com nome inválido deve retornar erro")]
    [Trait("Category", "Unit Test - CriarContatoCommandHandler (Cadastros)")]
    public async Task ExecuteAsync_ContatoComNomeInvalido_DeveRetornarErro()
    {
        // Arrange
        var command = new CriarContatoCommandBuilder()
            .ComNomeInvalido()
            .Build();

        // Act
        var result = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse("deve retornar erro");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Adicionar(It.IsAny<Contato>()), Times.Never);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }

    [Fact(DisplayName = "Criar contato com email inválido deve retornar erro")]
    [Trait("Category", "Unit Test - CriarContatoCommandHandler (Cadastros)")]
    public async Task ExecuteAsync_ContatoComEmailInvalido_DeveRetornarErro()
    {
        // Arrange
        var command = new CriarContatoCommandBuilder()
            .ComEmailInvalido()
            .Build();

        // Act
        var result = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse("deve retornar erro");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Adicionar(It.IsAny<Contato>()), Times.Never);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }

    [Fact(DisplayName = "Criar contato com telefone inválido deve retornar erro")]
    [Trait("Category", "Unit Test - CriarContatoCommandHandler (Cadastros)")]
    public async Task ExecuteAsync_ContatoComTelefoneInvalido_DeveRetornarErro()
    {
        // Arrange
        var command = new CriarContatoCommandBuilder()
            .ComTelefoneInvalido()
            .Build();

        // Act
        var result = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse("deve retornar erro");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Adicionar(It.IsAny<Contato>()), Times.Never);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }
}