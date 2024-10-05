using System.Net;
using Contatos.Api.IntegrationTest.Fixtures;
using FluentAssertions;

namespace Contatos.Api.IntegrationTest.Controllers;

public class ContatoControllerIntegrationTest : IClassFixture<IntegrationTestFixture>
{
    private readonly IntegrationTestFixture _fixture;

    public ContatoControllerIntegrationTest(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Listar contatos deve retornar Http 200")]
    [Trait("Category", "Integration Test - ContatoController")]
    public async Task Listar_Contatos_DeveRetornarHttp200()
    {
        // Arrange && Act
        var result = await _fixture.Client.GetAsync("/api/v1/contatos");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK, "deve retornar Http 200");
    }

    [Fact(DisplayName = "Obter contato por id deve retornar Http 200")]
    [Trait("Category", "Integration Test - ContatoController")]
    public async Task Obter_ContatoPorId_DeveRetornarHttp200()
    {
        // Arrange
        var contato = _fixture.ObterContatoValido();

        // Act
        var result = await _fixture.Client.GetAsync($"/api/v1/contatos/{contato.Id}");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK, "deve retornar Http 200");
    }
}