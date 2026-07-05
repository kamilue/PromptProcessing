using Microsoft.EntityFrameworkCore;
using Prompt.Domain.Entities;

namespace Prompt.Infrastructure.Persistence;

public class PromptDbContext : DbContext
{
    public PromptDbContext(DbContextOptions<PromptDbContext> options)
        : base(options)
    {
    }

    public DbSet<PromptJob> PromptJobs => Set<PromptJob>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PromptDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetAuditFields();

        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        SetAuditFields();

        return base.SaveChanges();
    }

    private void SetAuditFields()
    {
        var entries = ChangeTracker
            .Entries<PromptJob>()
            .Where(e => e.State == EntityState.Added);

        foreach (var entry in entries)
        {
            entry.Entity.CreatedAtUtc = DateTimeOffset.UtcNow;
        }
    }
}