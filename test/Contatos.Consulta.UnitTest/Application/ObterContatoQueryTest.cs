using Contatos.Consulta.Api.Application.DTOs;
using Contatos.Consulta.Api.Application.Queries;
using Contatos.Consulta.Api.Domain.Repositories;
using Contatos.Consulta.UnitTest.Builders;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace Contatos.Consulta.UnitTest.Application;

public class ObterContatoQueryTest
{
    private readonly AutoMocker _mocker;
    private readonly IObterContatoQuery _query;

    public ObterContatoQueryTest()
    {
        _mocker = new AutoMocker();
        _query = _mocker.CreateInstance<ObterContatoQuery>();
    }

    [Fact(DisplayName = "Obter contato, deve retornar o contato conforme o Id informado")]
    [Trait("Category", "Unit Test - ListarContatoUseCase")]
    public async Task ExecuteAsync_ObterContato_DeveRetornarContato()
    {
        // Arrange
        var contato = new ContatoBuilder().Build();

        _mocker.GetMock<IContatoRepository>()
            .Setup(r => r.ObterContatoPorIdAsync(contato.Id))
            .ReturnsAsync(contato);

        // Act
        var result = await _query.ExecuteAsync(contato.Id);

        // Assert
        result.Should().NotBeNull("deve retornar um contato");
        result.Should().BeOfType<ObterContatoOutput?>("o retorno deve ser do tipo {0}", nameof(ObterContatoOutput));
        result!.Id.Should().Be(contato.Id, "o Id do contato deve ser igual a {0}", contato.Id);
        result.Email.Should().Be(contato.Email?.Endereco, "o E-mail do contato deve ser igual a {0}",
            contato.Email?.Endereco);
        result.Nome.Should().Be(contato.Nome.PrimeiroNome, "o Nome do contato deve ser igual a {0}",
            contato.Nome.PrimeiroNome);
        result.Sobrenome.Should().Be(contato.Nome.Sobrenome, "o Sobrenome do contato deve ser igual a {0}",
            contato.Nome.Sobrenome);
        result.Telefones.Should().HaveCount(contato.Telefones.Count, "a quantidade de telefones deve ser igual a {0}",
            contato.Telefones.Count);
        _mocker.GetMock<IContatoRepository>()
            .Verify(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact(DisplayName = "Obter contato que não existe deve retornar null")]
    [Trait("Category", "Unit Test - ListarContatoUseCase")]
    public async Task ExecuteAsync_ObterContatoNaoExiste_DeveRetornarNull()
    {
        // Arrange
        _mocker.GetMock<IContatoRepository>()
            .Setup(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(() => null);

        // Act
        var result = await _query.ExecuteAsync(It.IsAny<Guid>());

        // Assert
        result.Should().BeNull("deve retornar null");
        _mocker.GetMock<IContatoRepository>()
            .Verify(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()), Times.Once);
    }
}