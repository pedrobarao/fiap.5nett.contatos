using Contatos.Application.DTOs.Outputs;
using Contatos.Application.UseCases;
using Contatos.Domain.Repositories;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Test.Commons.Builders.Domain.Entities;

namespace Contatos.Application.Test.UseCases;

public class ObterContatoUseCaseTest
{
    private readonly AutoMocker _mocker;
    private readonly ObterContatoUseCase _useCase;

    public ObterContatoUseCaseTest()
    {
        _mocker = new AutoMocker();
        _useCase = _mocker.CreateInstance<ObterContatoUseCase>();
    }

    [Fact(DisplayName = "Obter contato, deve retornar o contato conforme o Id informado")]
    [Trait("Category", "ListarContatoUseCase")]
    public async Task ExecuteAsync_ObterContato_DeveRetornarContato()
    {
        // Arrange
        var contato = new ContatoBuilder().Build();

        _mocker.GetMock<IContatoRepository>()
            .Setup(r => r.ObterContatoPorIdAsync(contato.Id))
            .ReturnsAsync(contato);

        // Act
        var result = await _useCase.ExecuteAsync(contato.Id);

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
    [Trait("Category", "ListarContatoUseCase")]
    public async Task ExecuteAsync_ObterContatoNaoExiste_DeveRetornarNull()
    {
        // Arrange
        _mocker.GetMock<IContatoRepository>()
            .Setup(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(() => null);

        // Act
        var result = await _useCase.ExecuteAsync(It.IsAny<Guid>());

        // Assert
        result.Should().BeNull("deve retornar null");
        _mocker.GetMock<IContatoRepository>()
            .Verify(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()), Times.Once);
    }
}