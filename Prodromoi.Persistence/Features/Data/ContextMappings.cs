using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prodromoi.Core.Features;

namespace Prodromoi.Persistence.Features.Data;

public static class ContextMappings
{
    public static void AddAuditRelationship<T>(this EntityTypeBuilder<T> entity) 
        where T : AuditEntity
    {
        entity
            .HasMany(act => act.AuditEntries)
            .WithOne()
            .HasForeignKey(ent => ent.SourceId);
    }
}