using Commons.Domain.Data;
using Contatos.SharedKernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contatos.Atualizacao.Api.Infra.Data;

public class ContatoDbContext : DbContext, IUnitOfWork
{
    public ContatoDbContext(DbContextOptions<ContatoDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Contato> Contatos { get; set; }

    public async Task<bool> Commit()
    {
        return await SaveChangesAsync() > 0;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.Cascade;

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContatoDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}