using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Prodromoi.Core.Features;
using Prodromoi.DomainModel.Model;
using Prodromoi.DomainModel.Model.Attendance;
using Prodromoi.DomainModel.Model.Formations;
using Prodromoi.DomainModel.Model.Members;

namespace Prodromoi.Persistence.Features.Data;

public class CoreContext : ProdromoiBaseDbContext
{
        
    public CoreContext(DbContextOptions options) : base(options)
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        ChangeTracker.StateChanged += GetChanges;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Actor>()
            .AddAuditRelationship();
        BuildAttendanceModel(modelBuilder);
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<AuditEntry> AuditEntries { get; set; }
    public DbSet<Actor> Actors { get; set; }

    #region attendance

    public DbSet<Member> Members { get; set; }
    public DbSet<RecordedAttendance> RecordedAttendances { get; set; }
    public DbSet<SectionRecordedAttendance> SectionRecordedAttendances { get; set; }

    private static void BuildAttendanceModel(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Member>()
            .AddAuditRelationship();
        
        modelBuilder
            .Entity<SectionRecordedAttendance>()
            .AddAuditRelationship();
        
        modelBuilder
            .Entity<RecordedAttendance>()
            .HasKey(ra => new
            {
                ra.SectionAttendanceId, 
                ra.MemberId
            });
    }
    
    #endregion

    #region formations

    public DbSet<Formation> Formations { get; set; }
    public DbSet<FormationSection> FormationSections { get; set; }

    private static void BuildFormationModel(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Formation>()
            .AddAuditRelationship();
        
        modelBuilder
            .Entity<FormationSection>()
            .AddAuditRelationship();
        
    }

    #endregion
    
    #region audit_entry_management

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new CancellationToken())
    {
        //Save all entities that aren't audit entries
        var changesResult = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken).Result;
        var auditsResult = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken).Result;
        return Task.FromResult(changesResult + auditsResult);

    }

    private void GetChanges(object? sender, EntityEntryEventArgs e)
    {
        var changes = ChangeTracker.Entries();
        if (e is not EntityStateChangedEventArgs mappedType) return;
        
        if (mappedType.NewState != EntityState.Unchanged || mappedType.OldState == EntityState.Unchanged) return;

        if(!IsAuditEntity(mappedType.Entry.Entity.GetType())) return;

        foreach (var audit in ((AuditEntity)mappedType.Entry.Entity).PendingAuditEntries.Select(entry 
                     => AuditEntry.Create(
                     entry.Actor,
                     entry.Entry,
                     mappedType.Entry.Entity.GetType().ToString(),
                     ((AuditEntity) mappedType.Entry.Entity).Id)))
        {
            Set<AuditEntry>().Add(audit);
            Entry(audit).State = EntityState.Added;
        }
        
    }
    
    public bool IsAuditEntity(Type entityType)
    {
        return entityType.IsSubclassOf(typeof(AuditEntity))
               || entityType == typeof(AuditEntity);
    }

    #endregion
    
}