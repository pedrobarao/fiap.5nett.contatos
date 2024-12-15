using Contatos.Consulta.Api;
using Contatos.Consulta.IntegrationTest.Config;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Contatos.Consulta.IntegrationTest.Fixtures;

public class IntegrationTestFixture : IAsyncLifetime
{
    private readonly IServiceScope _scope;
    private readonly CustomWebApplicationFactory<ConsultasProgram> _webApplication;
    public readonly HttpClient Client;

    public IntegrationTestFixture()
    {
        _webApplication = new CustomWebApplicationFactory<ConsultasProgram>();

        _scope = _webApplication.Services.CreateScope();

        Client = _webApplication.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = true,
            HandleCookies = true,
            MaxAutomaticRedirections = 7
        });
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        _webApplication?.Dispose();
        _scope.Dispose();
        Client.Dispose();

        return Task.CompletedTask;
    }
}