using Prodromoi.Core.Features;

namespace Prodromoi.DomainModel.Audit;

public class AuditEntry : Entity<long>
{
    public DateTime Timestamp { get; private set; } = DateTime.UnixEpoch;
    public string SourceType { get; private set; } = string.Empty;
    public long? SourceId { get; private set; }
    public string Actor { get; private set; } = string.Empty;
    public string Entry { get; private set; } = string.Empty;

    private AuditEntry(){}

    public static AuditEntry Create(
        string sourceType,
        string actor,
        string entry,
        long? sourceId = null)
    {
        var obj = new AuditEntry
        {
            Timestamp = DateTime.Now,
            SourceType = sourceType,
            SourceId = sourceId,
            Entry = entry,
            Actor = actor
        };

        return obj;
    }
    
}