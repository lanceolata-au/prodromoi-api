using Microsoft.EntityFrameworkCore;
using Prodromoi.Core.Features;
using Prodromoi.DomainModel.Model;

namespace Prodromoi.Service.Features.Data;

public class CoreContext : ProdromoiBaseDbContext
{

    public CoreContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<AuditEntry> AuditEntries { get; set; }

    public DbSet<Actor> Actors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Actor>()
            .AddAuditRelationship<Actor, int>();

        base.OnModelCreating(modelBuilder);
    }
}