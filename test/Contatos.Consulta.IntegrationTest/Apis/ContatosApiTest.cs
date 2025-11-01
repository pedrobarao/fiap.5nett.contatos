using System.Net;
using Contatos.Consulta.IntegrationTest.Fixtures;
using FluentAssertions;

namespace Contatos.Consulta.IntegrationTest.Apis;

public class ContatosApiTest : IClassFixture<IntegrationTestFixture>
{
    private const string ApiVersion = "api-version=1.0";
    private readonly IntegrationTestFixture _fixture;

    public ContatosApiTest(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Listar contatos deve retornar Http 200")]
    [Trait("Category", "Integration Test - ContatosApi (Consultas)")]
    public async Task Listar_Contatos_DeveRetornarHttp200()
    {
        // Arrange && Act
       // var result = await _fixture.Client.GetAsync($"/api/contatos?{ApiVersion}&pageSize=100&pageIndex=0");

        // Assert
//        result.StatusCode.Should().Be(HttpStatusCode.OK, "deve retornar Http 200");
    }

    [Fact(DisplayName = "Obter contato por id deve retornar Http 404")]
    [Trait("Category", "Integration Test - ContatosApi (Consultas)")]
    public async Task Obter_ContatoPorId_DeveRetornarHttp404()
    {
        // Arrange & Act
       // var result = await _fixture.Client.GetAsync($"/api/contatos/{Guid.NewGuid()}?{ApiVersion}");

        // Assert
//        result.StatusCode.Should().Be(HttpStatusCode.NotFound, "deve retornar Http 404");
    }
}