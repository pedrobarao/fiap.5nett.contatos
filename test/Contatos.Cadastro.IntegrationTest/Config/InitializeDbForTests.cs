using Contatos.Cadastro.Api.Infra.Data;
using Contatos.Test.Commons.Cadastros.Builders;

namespace Contatos.Cadastro.IntegrationTest.Config;

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