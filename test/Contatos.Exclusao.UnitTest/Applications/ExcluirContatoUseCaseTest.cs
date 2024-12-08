using Contatos.Exclusao.UnitTest.Builders;
using Moq;
using Moq.AutoMock;

namespace Contatos.Exclusao.UnitTest.Applications;

public class ExcluirContatoUseCaseTest
{
    private readonly AutoMocker _mocker;
    private readonly IExcluirContatoUseCase _useCase;

    public ExcluirContatoUseCaseTest()
    {
        _mocker = new AutoMocker();
        _useCase = _mocker.CreateInstance<ExcluirContatoUseCase>();
        _mocker.GetMock<IContatoRepository>()
            .Setup(r => r.UnitOfWork.Commit())
            .ReturnsAsync(() => true);
    }

    [Fact(DisplayName = "Excluir contato deve excluir com sucesso")]
    [Trait("Category", "Unit Test - ExcluirContatoUseCase")]
    public async Task ExecuteAsync_ContatoValido_DeveExcluirContatoComSucesso()
    {
        // Arrange
        var contato = new ContatoBuilder().Build();
        var input = contato.Id;

        _mocker.GetMock<IContatoRepository>()
            .Setup(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(contato);

        // Act
        var act = async () => await _useCase.ExecuteAsync(input);

        // Assert
        await act.Should().NotThrowAsync("deve excluir o contato com sucesso");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()), Times.Once);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Excluir(It.IsAny<Contato>()), Times.Once);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
    }

    [Fact(DisplayName = "Excluir contato que não existe deve excluir retornar ecxeção de domínio")]
    [Trait("Category", "Unit Test - ExcluirContatoUseCase")]
    public async Task ExecuteAsync_ContatoNaoExiste_DeveRetornarExcecaoDominio()
    {
        // Act && Arrange
        var act = async () => await _useCase.ExecuteAsync(Guid.NewGuid());

        // Assert
        await act.Should().NotThrowAsync("deve excluir o contato com sucesso");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()), Times.Once);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Excluir(It.IsAny<Contato>()), Times.Never);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }
}