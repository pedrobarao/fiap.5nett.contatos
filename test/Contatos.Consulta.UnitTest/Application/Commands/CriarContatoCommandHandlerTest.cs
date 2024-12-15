using Contatos.Consulta.Api.Application.Commands;
using Contatos.Consulta.Api.Domain.Entities;
using Contatos.Consulta.Api.Domain.Repositories;
using Contatos.Test.Commons.Consultas.Builders;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace Contatos.Consulta.UnitTest.Application.Commands;

public class CriarContatoCommandHandlerTest
{
    private readonly CriarContatoCommandHandler _commandHandler;
    private readonly AutoMocker _mocker;

    public CriarContatoCommandHandlerTest()
    {
        _mocker = new AutoMocker();
        _commandHandler = _mocker.CreateInstance<CriarContatoCommandHandler>();
    }

    [Fact(DisplayName = "Criar contato, deve atualizar com sucesso")]
    [Trait("Category", "Unit Test - AtualizarContatoCommandHanlder (Consultas)")]
    public async Task Handle_Criar_DeveCriarContatoComSucesso()
    {
        // Arrange
        var command = new CriarContatoCommandBuilder().Build();

        // Act
        var result = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue("o contato deve ser atualizado");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Criar(It.IsAny<Contato>()), Times.Once);
    }
}