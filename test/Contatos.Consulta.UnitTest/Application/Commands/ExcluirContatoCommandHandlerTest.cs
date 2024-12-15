using Contatos.Consulta.Api.Application.Commands;
using Contatos.Consulta.Api.Domain.Repositories;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace Contatos.Consulta.UnitTest.Application.Commands;

public class ExcluirContatoCommandHandlerTest
{
    private readonly ExcluirContatoCommandHandler _commandHandler;
    private readonly AutoMocker _mocker;

    public ExcluirContatoCommandHandlerTest()
    {
        _mocker = new AutoMocker();
        _commandHandler = _mocker.CreateInstance<ExcluirContatoCommandHandler>();
    }

    [Fact(DisplayName = "Excluir contato, deve excluir com sucesso")]
    [Trait("Category", "Unit Test - ExcluirContatoCommandHanlder (Consultas)")]
    public async Task Handle_Criar_DeveCriarContatoComSucesso()
    {
        // Arrange
        var command = new ExcluirContatoCommand
        {
            Id = Guid.NewGuid()
        };

        // Act
        var result = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue("o contato deve ser atualizado");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Excluir(It.IsAny<Guid>()), Times.Once);
    }
}