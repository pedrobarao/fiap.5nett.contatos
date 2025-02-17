using Contatos.Cadastro.Api;
using Contatos.Cadastro.Api.Domain.Entities;
using Contatos.Cadastro.Api.Infra.Data;
using Contatos.Cadastro.IntegrationTest.Config;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Contatos.Cadastro.IntegrationTest.Fixtures;

public class IntegrationTestFixture : IAsyncLifetime
{
    private readonly IServiceScope _scope;
    private readonly CustomWebApplicationFactory<CadastrosProgram> _webApplication;
    public readonly HttpClient Client;

    public IntegrationTestFixture()
    {
        _webApplication = new CustomWebApplicationFactory<CadastrosProgram>();

        _scope = _webApplication.Services.CreateScope();

        Client = _webApplication.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = true,
            HandleCookies = true,
            MaxAutomaticRedirections = 7
        });
    }

    public async Task InitializeAsync()
    {
        var db = GetDbContext();
        await InitializeDbForTests.GenerateData(db);
    }

    public Task DisposeAsync()
    {
        _webApplication?.Dispose();
        _scope.Dispose();
        Client.Dispose();

        return Task.CompletedTask;
    }

    private ContatoDbContext GetDbContext()
    {
        var scopedServices = _scope.ServiceProvider;
        return scopedServices.GetRequiredService<ContatoDbContext>();
    }

    public Contato ObterContatoValidoDb()
    {
        var db = GetDbContext();
        return db.Contatos.FirstOrDefault()!;
    }
}