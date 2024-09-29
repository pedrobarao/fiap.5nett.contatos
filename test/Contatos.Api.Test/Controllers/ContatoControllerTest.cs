using Commons.Domain.Communication;
using Contatos.Api.Controllers;
using Contatos.Application.DTOs.Outputs;
using Contatos.Application.UseCases.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using Test.Commons.Builders.Application.DTOs.Outputs;

namespace Contatos.Api.Test.Controllers;

public class ContatoControllerTest
{
    private readonly AutoMocker _mocker;
    private readonly ContatoController _controller;

    public ContatoControllerTest()
    {
        _mocker = new AutoMocker();
        _controller = _mocker.CreateInstance<ContatoController>();
    }

    [Fact(DisplayName = "Listar contatos cadastrados deve retornar Http 200 com a lista de contatos")]
    [Trait("Category", "ContatoController")]
    public async Task Listar_ContatosCadastrados_DeveRetornarListaContatosEHttp200()
    {
        // Arrange
        var output = new ObterContatoOutputBuilder().Build(3);
        var pagedResult = new PagedResult<ObterContatoOutput>(output,
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>());

        _mocker.GetMock<IListarContatoUseCase>()
            .Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(pagedResult);

        // Act
        var result = await _controller.Listar();

        // Assert
        result.Should()
            .BeOfType<OkObjectResult>()
            .Which
            .StatusCode
            .Should()
            .Be(StatusCodes.Status200OK, "deve retornar Http 200");

        result.As<OkObjectResult>()
            .Value
            .Should()
            .Be(pagedResult, "deve retornar a lista de contatos");
    }

    [Fact(DisplayName = "Listar contatos não encontrados deve retornar Http 200 com a lista lista vazia")]
    [Trait("Category", "ContatoController")]
    public async Task Listar_ContatosNaoEncontrados_DeveRetornarListaVaziaHttp200()
    {
        // Arrange
        var pagedResult = new PagedResult<ObterContatoOutput>([],
            0,
            0,
            0,
            It.IsAny<string>());

        _mocker.GetMock<IListarContatoUseCase>()
            .Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(pagedResult);

        // Act
        var result = await _controller.Listar();

        // Assert
        result.Should()
            .BeOfType<OkObjectResult>()
            .Which
            .StatusCode
            .Should()
            .Be(StatusCodes.Status200OK, "deve retornar Http 200");

        result.As<OkObjectResult>()
            .Value
            .Should()
            .Be(pagedResult, "deve retornar a lista vazia");
    }
    
    [Fact(DisplayName = "Obter contato por id deve retornar o contato e Http 200")]
    [Trait("Category", "ContatoController")]
    public async Task Obter_ContatoExisteNaBaseDeDados_DeveRetornarContatoEHttp200()
    {
        // Arrange
        var output = new ObterContatoOutputBuilder().Build();
        _mocker.GetMock<IObterContatoUseCase>()
            .Setup(u => u.ExecuteAsync(It.IsAny<Guid>()))
            .ReturnsAsync(output);

        // Act
        var result = await _controller.Obter(It.IsAny<Guid>());

        // Assert
        result.Should()
            .BeOfType<OkObjectResult>()
            .Which
            .StatusCode
            .Should()
            .Be(StatusCodes.Status200OK, "deve retornar Http 200");

        result.As<OkObjectResult>()
            .Value
            .Should()
            .Be(output, "deve retornar a o contato");
    }
    
    [Fact(DisplayName = "Obter contato por id não encontrado deve retornar Http 404")]
    [Trait("Category", "ContatoController")]
    public async Task Obter_ContatoNaoExisteNaBaseDeDados_DeveRetornarHttp404()
    {
        // Arrange
        _mocker.GetMock<IObterContatoUseCase>()
            .Setup(u => u.ExecuteAsync(It.IsAny<Guid>()))
            .ReturnsAsync(() => null);

        // Act
        var result = await _controller.Obter(It.IsAny<Guid>());

        // Assert
        result.Should()
            .BeOfType<NotFoundResult>()
            .Which
            .StatusCode
            .Should()
            .Be(StatusCodes.Status404NotFound, "deve retornar Http 404");
    }
}