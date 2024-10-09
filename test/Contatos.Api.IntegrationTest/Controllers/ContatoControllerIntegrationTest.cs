using System.Net;
using System.Net.Http.Json;
using Contatos.Api.IntegrationTest.Fixtures;
using Contatos.Application.DTOs.Inputs;
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
        var contato = _fixture.ObterContatoValidoDb();

        // Act
        var result = await _fixture.Client.GetAsync($"/api/v1/contatos/{contato.Id}");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK, "deve retornar Http 200");
    }

    [Fact(DisplayName = "Criar contato deve retornar Http 201")]
    [Trait("Category", "Integration Test - ContatoController")]
    public async Task Criar_Contato_DeveRetornarHttp200()
    {
        // Arrange
        var contato = _fixture.GerarContatoValido();
        var payload = new NovoContatoInput
        {
            Nome = contato.Nome.PrimeiroNome,
            Sobrenome = contato.Nome.Sobrenome,
            Email = contato.Email!.Endereco,
            Telefones = contato.Telefones.ToList()
        };

        // Act
        var result = await _fixture.Client.PostAsJsonAsync("/api/v1/contatos", payload);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created, "deve retornar Http 201");
    }

    [Fact(DisplayName = "Atualizar contato deve retornar Http 204")]
    [Trait("Category", "Integration Test - ContatoController")]
    public async Task Atualizar_Contato_DeveRetornarHttp204()
    {
        // Arrange
        var contato = _fixture.ObterContatoValidoDb();
        var atualizarContato = _fixture.GerarContatoValido();
        var payload = new AtualizarContatoInput
        {
            Id = contato.Id,
            Nome = atualizarContato.Nome.PrimeiroNome,
            Sobrenome = atualizarContato.Nome.Sobrenome,
            Email = atualizarContato.Email!.Endereco,
            Telefones = atualizarContato.Telefones.ToList()
        };

        // Act
        var result = await _fixture.Client.PutAsJsonAsync($"/api/v1/contatos/{contato.Id}", payload);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent, "deve retornar Http 204");
    }

    [Fact(DisplayName = "Excluir contato deve retornar Http 204")]
    [Trait("Category", "Integration Test - ContatoController")]
    public async Task Excluir_Contato_DeveRetornarHttp204()
    {
        // Arrange
        var contato = _fixture.ObterContatoValidoDb();

        // Act
        var result = await _fixture.Client.DeleteAsync($"/api/v1/contatos/{contato.Id}");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent, "deve retornar Http 204");
    }
}