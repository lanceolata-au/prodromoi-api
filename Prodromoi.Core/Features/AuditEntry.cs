using System.ComponentModel.DataAnnotations.Schema;

namespace Prodromoi.Core.Features;

public class AuditEntry : Entity<long>
{
    public DateTime Timestamp { get; private set; } = DateTime.UnixEpoch;
    public string SourceType { get; private set; } = string.Empty;
    public int SourceId { get; private set; } = 0;
    
    public string Actor { get; private set; } = string.Empty;
    public string Entry { get; private set; } = string.Empty;

    private AuditEntry(){}

    public static AuditEntry Create(
        string actor,
        string entry, 
        string sourceType,
        int sourceId)
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

}