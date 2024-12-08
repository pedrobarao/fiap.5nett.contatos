using Commons.Domain.Communication;
using Contatos.Consulta.Api.Application.DTOs;
using Contatos.Consulta.Api.Application.Queries;
using Contatos.Consulta.Api.Domain.Repositories;
using Contatos.Consulta.UnitTest.Builders;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace Contatos.Consulta.UnitTest.Application;

public class ListarContatoQueryTest
{
    private readonly AutoMocker _mocker;
    private readonly IListarContatoQuery _query;

    public ListarContatoQueryTest()
    {
        _mocker = new AutoMocker();
        _query = _mocker.CreateInstance<ListarContatoQuery>();
    }

    [Fact(DisplayName = "Listar contados, deve retornar lista de contatos")]
    [Trait("Category", "Unit Test - ListarContatoUseCase")]
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
        var result = await _query.ExecuteAsync(pageSize, pageIndex, It.IsAny<string>());

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
    [Trait("Category", "Unit Test - ListarContatoUseCase")]
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
        var result = await _query.ExecuteAsync(pageSize, pageIndex, It.IsAny<string>());

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