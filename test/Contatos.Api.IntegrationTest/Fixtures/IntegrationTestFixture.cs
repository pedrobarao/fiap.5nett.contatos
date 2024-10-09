using Contatos.Api.IntegrationTest.Config;
using Contatos.Domain.Entities;
using Contatos.Infra.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Test.Commons.Builders.Domain.Entities;

namespace Contatos.Api.IntegrationTest.Fixtures;

[CollectionDefinition(nameof(IntegrationTestCollection))]
public class IntegrationTestCollection : ICollectionFixture<IntegrationTestFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

public class IntegrationTestFixture : IAsyncLifetime
{
    private readonly IServiceScope _scope;
    private readonly CustomWebApplicationFactory<Program> _webApplication;
    public readonly HttpClient Client;

    public IntegrationTestFixture()
    {
        _webApplication = new CustomWebApplicationFactory<Program>();

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
    
    public Contato GerarContatoValido()
    {
        return new ContatoBuilder().Build();
    }
}