using Contatos.Infra.Data;
using Test.Commons.Builders.Domain.Entities;

namespace Contatos.Api.IntegrationTest.Config;

public static class InitializeDbForTests
{
    public static async Task GenerateData(ContatoDbContext context)
    {
        var contatos = new ContatoBuilder().Build();
        await context.Database.EnsureCreatedAsync();
        await context.Contatos.AddRangeAsync(contatos);
        await context.SaveChangesAsync();
    }
}