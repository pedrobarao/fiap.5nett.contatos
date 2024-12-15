using Contatos.Consulta.Api.Application.Commands;
using Contatos.Consulta.Api.Domain.Entities;
using Contatos.Consulta.Api.Domain.Repositories;
using Contatos.Test.Commons.Consultas.Builders;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace Contatos.Consulta.UnitTest.Application.Commands;

public class AtualizarContatoCommandHandlerTest
{
    private readonly AtualizarContatoCommandHandler _commandHandler;
    private readonly AutoMocker _mocker;

    public AtualizarContatoCommandHandlerTest()
    {
        _mocker = new AutoMocker();
        _commandHandler = _mocker.CreateInstance<AtualizarContatoCommandHandler>();
    }

    [Fact(DisplayName = "Atualizar contato, deve atualizar com sucesso")]
    [Trait("Category", "Unit Test - AtualizarContatoCommandHandler (Consultas)")]
    public async Task Handle_Atualizar_DeveAtualizarContatoComSucesso()
    {
        // Arrange
        var command = new AtualizarContatoCommandBuilder().Build();

        // Act
        var result = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue("o contato deve ser atualizado");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Atualizar(It.IsAny<Contato>()), Times.Once);
    }
}