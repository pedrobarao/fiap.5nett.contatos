using Contatos.Application.UseCases;
using Contatos.Domain.Entities;
using Contatos.Domain.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Http.Features;
using Moq;
using Moq.AutoMock;
using Test.Commons.Builders.Application.DTOs.Inputs;
using Test.Commons.Builders.Domain.Entities;

namespace Contatos.Application.Test.UseCases;

public class AtualizarContatoUseCaseTest
{
    private readonly AutoMocker _mocker;
    private readonly AtualizarContatoUseCase _useCase;

    public AtualizarContatoUseCaseTest()
    {
        _mocker = new AutoMocker();
        _useCase = _mocker.CreateInstance<AtualizarContatoUseCase>();
        _mocker.GetMock<IContatoRepository>()
            .Setup(r => r.UnitOfWork.Commit())
            .ReturnsAsync(() => true);
    }
    
    [Fact(DisplayName = "Atualizar contato com valores válidos deve atualizar com sucesso")]
    [Trait("Category", "AtualizarContatoUseCase")]
    public async Task ExecuteAsync_ContatoValido_DeveAtualizarContatoComSucesso()
    {
        // Arrange
        var contato = new ContatoBuilder().Build();
        
        var input = new AtualizarContatoInputBuilder()
            .ComIdContato(contato.Id)
            .Build();
        
        _mocker.GetMock<IContatoRepository>()
            .Setup(r => r.ObterContatoPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(contato);
        
        // Act
        var result = await _useCase.ExecuteAsync(input);

        // Assert
        result.IsSuccess.Should().BeTrue("o contato deve ser atualizado");
        _mocker.GetMock<IContatoRepository>().Verify(r => r.Atualizar(It.IsAny<Contato>()), Times.Once);
        _mocker.GetMock<IContatoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
    }
}