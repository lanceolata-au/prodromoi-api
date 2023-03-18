using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prodromoi.Core.Features;
using Prodromoi.Service.Extensions;

namespace Prodromoi.Service.Features.Data;

public static class ContextMappings
{
    public static void AddAuditRelationship<T, TId>(this EntityTypeBuilder<T> entity) 
        where T : AuditEntity<TId> 
        where TId : struct
    {
        entity
            .HasMany(act => act.AuditEntries)
            .WithOne()
            .HasForeignKey(ent => ent.SourceId);
    }
}