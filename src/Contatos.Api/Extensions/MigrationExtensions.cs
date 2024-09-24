using System.Diagnostics.CodeAnalysis;
using Contatos.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Contatos.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class MigrationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ContatoDbContext>();
        dbContext.Database.Migrate();
    }
}