using Microsoft.EntityFrameworkCore;
using Prodromoi.Core.Features;
using Prodromoi.DomainModel.Model.Events;

namespace Prodromoi.Service.Features.Data;

public class CoreContext : ProdromoiBaseDbContext
{

    public CoreContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>()
            .HasMany(evnt => evnt.AuditEntries)
            .WithOne()
            .HasForeignKey("SourceId")
            .HasForeignKey(ent => ent.SourceId);

        base.OnModelCreating(modelBuilder);
    }
}