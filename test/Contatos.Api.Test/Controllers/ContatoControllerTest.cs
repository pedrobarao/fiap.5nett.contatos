using Commons.Domain.Communication;
using Commons.Domain.DomainObjects;
using Contatos.Api.Controllers;
using Contatos.Application.DTOs.Inputs;
using Contatos.Application.DTOs.Outputs;
using Contatos.Application.UseCases.Interfaces;
using Contatos.Domain.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using Test.Commons.Builders.Application.DTOs.Inputs;
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

        _mocker.GetMock<IListarContatoUseCase>()
            .Verify(r => r.ExecuteAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once);
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

        _mocker.GetMock<IListarContatoUseCase>()
            .Verify(r => r.ExecuteAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once);
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

        _mocker.GetMock<IObterContatoUseCase>().Verify(r => r.ExecuteAsync(It.IsAny<Guid>()), Times.Once);
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

        _mocker.GetMock<IObterContatoUseCase>().Verify(r => r.ExecuteAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact(DisplayName = "Criar contato com dados inválidos deve retornar Http 400")]
    [Trait("Category", "ContatoController")]
    public async Task Criar_ContatoInvalido_DeveRetornarHttp400()
    {
        // Arrange
        _mocker.GetMock<ICadastrarContatoUseCase>()
            .Setup(u => u.ExecuteAsync(It.IsAny<NovoContatoInput>()))
            .ReturnsAsync(Result<ContatoCriadoOutput>.Fail(It.IsAny<string>()));

        // Act
        var result = await _controller.Criar(It.IsAny<NovoContatoInput>());

        // Assert
        result.Should()
            .BeOfType<BadRequestObjectResult>()
            .Which
            .StatusCode
            .Should()
            .Be(StatusCodes.Status400BadRequest, "deve retornar Http 400");

        _mocker.GetMock<ICadastrarContatoUseCase>()
            .Verify(r => r.ExecuteAsync(It.IsAny<NovoContatoInput>()), Times.Once);
    }

    [Fact(DisplayName = "Criar contato com dados válidos deve retornar o id do contato e Http 201")]
    [Trait("Category", "ContatoController")]
    public async Task Criar_ContatoValido_DeveRetornarIdContatoEHttp201()
    {
        // Arrange
        var output = new ContatoCriadoOutputBuilder().Build();
        _mocker.GetMock<ICadastrarContatoUseCase>()
            .Setup(u => u.ExecuteAsync(It.IsAny<NovoContatoInput>()))
            .ReturnsAsync(Result<ContatoCriadoOutput>.Success(output));

        // Act
        var result = await _controller.Criar(It.IsAny<NovoContatoInput>());

        // Assert
        result.Should()
            .BeOfType<CreatedResult>()
            .Which
            .StatusCode
            .Should()
            .Be(StatusCodes.Status201Created, "deve retornar Http 201");

        result.As<CreatedResult>()
            .Value
            .Should()
            .Be(output, "deve retornar a o id do contato criado");

        _mocker.GetMock<ICadastrarContatoUseCase>()
            .Verify(r => r.ExecuteAsync(It.IsAny<NovoContatoInput>()), Times.Once);
    }

    [Fact(DisplayName = "Atualizar contato com dados inválidos deve retornar Http 400")]
    [Trait("Category", "ContatoController")]
    public async Task Atualizar_ContatoInvalido_DeveRetornarHttp400()
    {
        // Arrange
        var input = new AtualizarContatoInputBuilder().Build();
        _mocker.GetMock<IAtualizarContatoUseCase>()
            .Setup(u => u.ExecuteAsync(input))
            .ReturnsAsync(Result.Fail(It.IsAny<string>()));

        // Act
        var result = await _controller.Atualizar(input.Id, input);

        // Assert
        result.Should()
            .BeOfType<BadRequestObjectResult>()
            .Which
            .StatusCode
            .Should()
            .Be(StatusCodes.Status400BadRequest, "deve retornar Http 400");

        _mocker.GetMock<IAtualizarContatoUseCase>()
            .Verify(r => r.ExecuteAsync(It.IsAny<AtualizarContatoInput>()), Times.Once);
    }

    [Fact(DisplayName = "Atualizar com id da rota diferente do id do contato no body deve retornar Http 400")]
    [Trait("Category", "ContatoController")]
    public async Task Atualizar_SolicitacaoComIdDaRotaDiferenteDoIdContatoDoBody_DeveRetornarHttp400()
    {
        // Arrange
        var input = new AtualizarContatoInputBuilder().Build();

        // Act
        var result = await _controller.Atualizar(Guid.NewGuid(), input);

        // Assert
        result.Should()
            .BeOfType<BadRequestObjectResult>()
            .Which
            .StatusCode
            .Should()
            .Be(StatusCodes.Status400BadRequest, "deve retornar Http 400");

        _mocker.GetMock<IAtualizarContatoUseCase>()
            .Verify(r => r.ExecuteAsync(It.IsAny<AtualizarContatoInput>()), Times.Never);
    }

    [Fact(DisplayName = "Atualizar contato válido deve retornar Http 204")]
    [Trait("Category", "ContatoController")]
    public async Task Atualizar_ContatoValido_DeveRetornarHttp204()
    {
        // Arrange
        var input = new AtualizarContatoInputBuilder().Build();
        _mocker.GetMock<IAtualizarContatoUseCase>()
            .Setup(u => u.ExecuteAsync(input))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await _controller.Atualizar(input.Id, input);

        // Assert
        result.Should()
            .BeOfType<NoContentResult>()
            .Which
            .StatusCode
            .Should()
            .Be(StatusCodes.Status204NoContent, "deve retornar Http 204");

        _mocker.GetMock<IAtualizarContatoUseCase>().Verify(r => r.ExecuteAsync(input), Times.Once);
    }

    [Fact(DisplayName = "Excluir contato que não existe na base de dados deve retornar exceção de domínio")]
    [Trait("Category", "ContatoController")]
    public async Task Excluir_ContatoNaoExiste_DeveRetornarExcecaoDominio()
    {
        // Arrange
        _mocker.GetMock<IExcluirContatoUseCase>()
            .Setup(u => u.ExecuteAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new DomainException());

        // Act
        var act = async () => await _controller.Excluir(It.IsAny<Guid>());

        // Assert
        await act.Should().ThrowExactlyAsync<DomainException>("deve retornar uma exceção de domínio");

        _mocker.GetMock<IExcluirContatoUseCase>().Verify(r => r.ExecuteAsync(It.IsAny<Guid>()), Times.Once);
    }
    
    [Fact(DisplayName = "Excluir contato deve excluí-lo e retornar Http 204")]
    [Trait("Category", "ContatoController")]
    public async Task Excluir_Contato_DeveExcluirContatoERetornarHttp204()
    {
        // Arrange & Act
        var result = await _controller.Excluir(It.IsAny<Guid>());

        // Assert
        result.Should()
            .BeOfType<NoContentResult>()
            .Which
            .StatusCode
            .Should()
            .Be(StatusCodes.Status204NoContent, "deve retornar Http 204");

        _mocker.GetMock<IExcluirContatoUseCase>().Verify(r => r.ExecuteAsync(It.IsAny<Guid>()), Times.Once);
    }
}