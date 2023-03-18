using System.ComponentModel.DataAnnotations.Schema;

namespace Prodromoi.Core.Features;

public class AuditEntry : Entity<long>
{
    public DateTime Timestamp { get; private set; } = DateTime.UnixEpoch;
    public string SourceType { get; private set; } = string.Empty;
    public int? SourceId { get; private set; }
    
    public string Actor { get; private set; } = string.Empty;
    public string Entry { get; private set; } = string.Empty;

    private AuditEntry(){}

    public static AuditEntry Create(
        string sourceType,
        string actor,
        string entry, 
        int? sourceId = null)
    {
        var obj = new AuditEntry
        {
            Timestamp = DateTime.Now.ToUniversalTime(),
            SourceType = sourceType,
            Actor = actor,
            Entry = entry,
            SourceId = sourceId
        };

        return obj;
    }

    public void SetSourceId(int sourceId)
    {
        if (SourceId != null) return;
        SourceId = sourceId;
    }
    
}