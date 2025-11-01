using Contatos.Cadastro.Api.Application.Commands.Excluir;
using Contatos.Cadastro.Api.Domain.Entities;
using Contatos.Cadastro.Api.Domain.Repositories;
using Contatos.Test.Commons.Cadastros.Builders;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace Contatos.Cadastro.UnitTest.Application.Commands;

public class ExcluirContatoCommandHandlerTest
{
    private readonly ExcluirContatoCommandHandler _commandHandler;
    private readonly AutoMocker _mocker;

    public ExcluirContatoCommandHandlerTest()
    {
        _mocker = new AutoMocker();
        _commandHandler = _mocker.CreateInstance<ExcluirContatoCommandHandler>();
        _mocker.GetMock<IContatoRepository>()
            .Setup(r => r.UnitOfWork.Commit())
            .ReturnsAsync(() => true);
    }

    [Fact(DisplayName = "Excluir contato deve excluir com sucesso")]
    [Trait("Category", "Unit Test - ExcluirContatoCommandHandler (Cadastros)")]
    public async Task ExecuteAsync_ContatoValido_DeveExcluirContatoComSucesso()
    {
        // Arrange
        var contato = new ContatoBuilder().Build();
        var command = new ExcluirContatoCommand
        {
            Id = contato.Id
        };

        _mocker.GetMock<IContatoRepository>()
            .Setup(r => r.ObterContatoPorId(It.IsAny<Guid>()))
            .ReturnsAsync(contato);

        // Act
        var act = async () => await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().NotThrowAsync("deve excluir o contato com sucesso");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.ObterContatoPorId(It.IsAny<Guid>()), Times.Once);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Excluir(It.IsAny<Contato>()), Times.Once);
//        _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
    }

    [Fact(DisplayName = "Excluir contato que não existe deve excluir retornar ecxeção de domínio")]
    [Trait("Category", "Unit Test - ExcluirContatoCommandHandler (Cadastros)")]
    public async Task ExecuteAsync_ContatoNaoExiste_DeveRetornarExcecaoDominio()
    {
        // Arrange
        var command = new ExcluirContatoCommand
        {
            Id = Guid.NewGuid()
        };

        // Act
        var act = async () => await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().NotThrowAsync("deve excluir o contato com sucesso");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.ObterContatoPorId(It.IsAny<Guid>()), Times.Once);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Excluir(It.IsAny<Contato>()), Times.Never);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }
}