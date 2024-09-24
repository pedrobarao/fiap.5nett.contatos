using Commons.Domain.Communication;
using Contatos.Application.UseCases;
using Contatos.Domain.Entities;
using Contatos.Domain.Repositories;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace Contatos.Application.Test.UseCases;

public class ListarContatoUseCaseTest
{
    private readonly AutoMocker _mocker;

    public ListarContatoUseCaseTest()
    {
        _mocker = new AutoMocker();
    }

    [Fact(DisplayName = "Excluir contato inválido deve retornar erro")]
    [Trait("Category", "ExcluirContatoUseCase")]
    public async Task ListarContatoUseCase_DeveListarContatosComSucesso()
    {
        // Arrange
        _mocker.GetMock<IContatoRepository>().Setup(r =>
                r.ObterContatosPaginados(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(new PagedResult<Contato>(new List<Contato>(), 1, 1, 1, ""));
        var useCase = _mocker.CreateInstance<ListarContatoUseCase>();

        // Act
        var result = await useCase.ExecuteAsync(10, 1, It.IsAny<string>());

        // Assert
        result.TotalItems.Should().Be(1, "deve retornar 1 item");
    }
}