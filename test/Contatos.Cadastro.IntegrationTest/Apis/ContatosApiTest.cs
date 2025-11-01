using System.Net;
using System.Net.Http.Json;
using Contatos.Cadastro.Api.Application.Commands.Atualizar;
using Contatos.Cadastro.Api.Application.Commands.Criar;
using Contatos.Cadastro.IntegrationTest.Fixtures;
using Contatos.Cadastro.IntegrationTest.Helpers;
using FluentAssertions;

namespace Contatos.Cadastro.IntegrationTest.Apis;

public class ContatosApiTest //: IClassFixture<IntegrationTestFixture>
{
    private const string ApiVersion = "api-version=1.0";
    private readonly IntegrationTestFixture _fixture;

    // public ContatosApiTest(IntegrationTestFixture fixture)
    // {
    //     _fixture = fixture;
    // }

    [Fact(DisplayName = "Criar contato deve retornar Http 201")]
    [Trait("Category", "Integration Test - ContatosApi (Cadastros)")]
    public async Task Criar_Contato_DeveRetornarHttp200()
    {
        // // Arrange
        // var contato = ContatoHelper.ContatoValido();
        // var payload = new CriarContatoCommand
        // {
        //     Nome = contato.Nome.PrimeiroNome,
        //     Sobrenome = contato.Nome.Sobrenome,
        //     Email = contato.Email!.Endereco,
        //     Telefones = contato.Telefones.Select(s => new CriarContatoCommand.TelefoneCriacao
        //     {
        //         Ddd = s.Ddd,
        //         Numero = s.Numero,
        //         Tipo = s.Tipo
        //     }).ToList()
        // };
        //
        // // Act
        // var result = await _fixture.Client.PostAsJsonAsync($"/api/contatos?{ApiVersion}", payload);
        //
        // // Assert
        // result.StatusCode.Should().Be(HttpStatusCode.Created, "deve retornar Http 201");
    }

    [Fact(DisplayName = "Atualizar contato deve retornar Http 204")]
    [Trait("Category", "Integration Test - ContatosApi (Cadastros)")]
    public async Task Atualizar_Contato_DeveRetornarHttp204()
    {
        // // Arrange
        // var contato = _fixture.ObterContatoValidoDb();
        // var atualizarContato = ContatoHelper.ContatoValido();
        // var payload = new AtualizarContatoCommand
        // {
        //     Id = contato.Id,
        //     Nome = atualizarContato.Nome.PrimeiroNome,
        //     Sobrenome = atualizarContato.Nome.Sobrenome,
        //     Email = atualizarContato.Email!.Endereco,
        //     Telefones = contato.Telefones.Select(s => new AtualizarContatoCommand.TelefoneAtualizacao
        //     {
        //         Ddd = s.Ddd,
        //         Numero = s.Numero,
        //         Tipo = s.Tipo
        //     }).ToList()
        // };
        //
        // // Act
        // var result = await _fixture.Client.PutAsJsonAsync($"/api/contatos/{contato.Id}?{ApiVersion}", payload);
        //
        // // Assert
        // result.StatusCode.Should().Be(HttpStatusCode.NoContent, "deve retornar Http 204");
    }

    [Fact(DisplayName = "Excluir contato deve retornar Http 204")]
    [Trait("Category", "Integration Test - ContatosApi (Cadastros)")]
    public async Task Excluir_Contato_DeveRetornarHttp204()
    {
        // // Arrange
        // var contato = _fixture.ObterContatoValidoDb();
        //
        // // Act
        // var result = await _fixture.Client.DeleteAsync($"/api/contatos/{contato.Id}?{ApiVersion}");
        //
        // // Assert
        // result.StatusCode.Should().Be(HttpStatusCode.NoContent, "deve retornar Http 204");
    }
}