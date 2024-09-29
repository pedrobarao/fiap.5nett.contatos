using Contatos.Application.DTOs.Outputs;
using Contatos.Application.UseCases;
using Contatos.Application.UseCases.Interfaces;
using Contatos.Domain.Entities;
using Contatos.Domain.Repositories;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Test.Commons.Builders.Application.DTOs.Inputs;

namespace Contatos.Application.Test.UseCases;

public class CadastrarContatoUseCaseTest
{
    private readonly AutoMocker _mocker;
    private readonly ICadastrarContatoUseCase _useCase;

    public CadastrarContatoUseCaseTest()
    {
        _mocker = new AutoMocker();
        _useCase = _mocker.CreateInstance<CadastrarContatoUseCase>();
        _mocker.GetMock<IContatoRepository>()
            .Setup(r => r.UnitOfWork.Commit())
            .ReturnsAsync(() => true);
    }

    [Fact(DisplayName = "Cadastrar contato com valores válidos deve criar o contato com sucesso")]
    [Trait("Category", "CadastrarContatoUseCase")]
    public async Task ExecuteAsync_ContatoValido_DeveCriarContatoComSucesso()
    {
        // Arrange
        var input = new NovoContatoInputBuilder().Build();

        // Act
        var result = await _useCase.ExecuteAsync(input);

        // Assert
        result.IsSuccess.Should().BeTrue("o contato deve ser criado");
        result.Data.Should().BeOfType<ContatoCriadoOutput>($"o tipo deve ser ${nameof(ContatoCriadoOutput)}");
        result.Data!.Id.Should().NotBeEmpty("o id do contato deve ser gerado");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Adicionar(It.IsAny<Contato>()), Times.Once);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
    }

    [Fact(DisplayName = "Criar contato com nome inválido deve retornar erro")]
    [Trait("Category", "CadastrarContatoUseCase")]
    public async Task ExecuteAsync_ContatoComNomeInvalido_DeveRetornarErro()
    {
        // Arrange
        var input = new NovoContatoInputBuilder()
            .ComNomeInvalido()
            .Build();

        // Act
        var result = await _useCase.ExecuteAsync(input);

        // Assert
        result.IsSuccess.Should().BeFalse("deve retornar erro");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Adicionar(It.IsAny<Contato>()), Times.Never);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }

    [Fact(DisplayName = "Criar contato com email inválido deve retornar erro")]
    [Trait("Category", "CadastrarContatoUseCase")]
    public async Task ExecuteAsync_ContatoComEmailInvalido_DeveRetornarErro()
    {
        // Arrange
        var input = new NovoContatoInputBuilder()
            .ComEmailInvalido()
            .Build();

        // Act
        var result = await _useCase.ExecuteAsync(input);

        // Assert
        result.IsSuccess.Should().BeFalse("deve retornar erro");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Adicionar(It.IsAny<Contato>()), Times.Never);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }

    [Fact(DisplayName = "Criar contato com telefone inválido deve retornar erro")]
    [Trait("Category", "CadastrarContatoUseCase")]
    public async Task ExecuteAsync_ContatoComTelefoneInvalido_DeveRetornarErro()
    {
        // Arrange
        var input = new NovoContatoInputBuilder()
            .ComTelefoneInvalido()
            .Build();

        // Act
        var result = await _useCase.ExecuteAsync(input);

        // Assert
        result.IsSuccess.Should().BeFalse("deve retornar erro");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Adicionar(It.IsAny<Contato>()), Times.Never);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }
}