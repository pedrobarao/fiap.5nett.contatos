using Commons.Domain.Data;
using Contatos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contatos.Infra.Data;

public class ContatoDbContext : DbContext, IUnitOfWork
{
    public ContatoDbContext(DbContextOptions<ContatoDbContext> options) : base(options)
    {
        DisableTrackingScope();
    }

    public DbSet<Contato> Contatos { get; set; }

    public async Task<bool> Commit()
    {
        return await SaveChangesAsync() > 0;
    }

    public void DisableTrackingScope()
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public void EnableTrackingScope()
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        ChangeTracker.AutoDetectChangesEnabled = true;
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