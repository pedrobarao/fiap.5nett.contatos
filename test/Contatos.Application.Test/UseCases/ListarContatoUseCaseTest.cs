using Commons.Domain.Communication;
using Contatos.Application.DTOs.Outputs;
using Contatos.Application.UseCases;
using Contatos.Domain.Entities;
using Contatos.Domain.Repositories;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Test.Commons.Builders.Domain.Entities;

namespace Contatos.Application.Test.UseCases;

public class ListarContatoUseCaseTest
{
    private readonly AutoMocker _mocker;
    private readonly ListarContatoUseCase _useCase;

    public ListarContatoUseCaseTest()
    {
        _mocker = new AutoMocker();
        _useCase = _mocker.CreateInstance<ListarContatoUseCase>();
    }

    [Fact(DisplayName = "Listar contados, deve retornar lista de contatos")]
    [Trait("Category", "ListarContatoUseCase")]
    public async Task ExecuteAsync_ListarContatos_DeveRetornarListaDeContatos()
    {
        // Arrange
        var totalItems = 10;
        var pageSize = 10;
        var pageIndex = 1;
        var contato = new ContatoBuilder().Build(totalItems);

        var queryResult =
            new PagedResult<Contato>(contato.AsEnumerable(), totalItems, pageIndex, pageSize, It.IsAny<string>());

        _mocker.GetMock<IContatoRepository>()
            .Setup(r => r.ObterContatosPaginados(pageSize, pageIndex, It.IsAny<string>()))
            .ReturnsAsync(queryResult);

        // Act
        var result = await _useCase.ExecuteAsync(pageSize, pageIndex, It.IsAny<string>());

        // Assert
        result.Should()
            .BeOfType<PagedResult<ObterContatoOutput>>("deve ser do tipo {0}", nameof(PagedResult<ObterContatoOutput>));
        result.Items.Should().NotBeEmpty("a lista de contatos deve estar preenchida");
        result.PageIndex.Should().Be(queryResult.PageIndex, "a página deve ser igual a {0}", queryResult.PageIndex);
        result.PageSize.Should().Be(queryResult.PageSize, "a quantidade de itens por página deve ser igual a {0}",
            queryResult.PageSize);
        result.TotalPages.Should().Be(queryResult.TotalPages, "o total de páginas deve ser igual a {0}",
            queryResult.TotalPages);
        result.TotalItems.Should().Be(queryResult.TotalItems, "o total de itens deve ser igual a {0}",
            queryResult.TotalItems);
        result.Filter.Should().Be(queryResult.Filter, "o filtro deve ser igual a {0}", queryResult.Filter);
        _mocker.GetMock<IContatoRepository>()
            .Verify(r => r.ObterContatosPaginados(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once);
    }

    [Fact(DisplayName = "Listar contados que não existem, deve retornar lista vazia")]
    [Trait("Category", "ListarContatoUseCase")]
    public async Task ExecuteAsync_ListarContatosQuNaoExistem_DeveRetornarListaVazia()
    {
        // Arrange
        var totalItems = 0;
        var pageSize = 10;
        var pageIndex = 1;

        var queryResult =
            new PagedResult<Contato>([], totalItems, pageIndex, pageSize, It.IsAny<string>());

        _mocker.GetMock<IContatoRepository>()
            .Setup(r => r.ObterContatosPaginados(pageSize, pageIndex, It.IsAny<string>()))
            .ReturnsAsync(queryResult);

        // Act
        var result = await _useCase.ExecuteAsync(pageSize, pageIndex, It.IsAny<string>());

        // Assert
        result.Should()
            .BeOfType<PagedResult<ObterContatoOutput>>("deve ser do tipo {0}", nameof(PagedResult<ObterContatoOutput>));
        result.Items.Should().BeEmpty("a lista de contatos deve ser vazia");
        result.PageIndex.Should().Be(queryResult.PageIndex, "a página deve ser igual a {0}", queryResult.PageIndex);
        result.PageSize.Should().Be(queryResult.PageSize, "a quantidade de itens por página deve ser igual a {0}",
            queryResult.PageSize);
        result.TotalPages.Should().Be(queryResult.TotalPages, "o total de páginas deve ser igual a {0}",
            queryResult.TotalPages);
        result.TotalItems.Should().Be(queryResult.TotalItems, "o total de itens deve ser igual a {0}",
            queryResult.TotalItems);
        result.Filter.Should().Be(queryResult.Filter, "o filtro deve ser igual a {0}", queryResult.Filter);
        _mocker.GetMock<IContatoRepository>()
            .Verify(r => r.ObterContatosPaginados(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once);
    }
}