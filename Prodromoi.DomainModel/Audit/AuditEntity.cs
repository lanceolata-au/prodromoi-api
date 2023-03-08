using Prodromoi.Core.Features;

namespace Prodromoi.DomainModel.Audit;

public class AuditEntity<TId> : Entity<TId> where TId: struct
{
    public virtual List<AuditEntry> AuditEntries { get; private set; }
}