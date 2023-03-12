using Prodromoi.Core.Features;

namespace Prodromoi.DomainModel.Model.Events;

public class Event : AuditEntity<int>
{
    public string Name { get; private set; }
    public DateOnly Start { get; private set; }
    public DateOnly? End { get; private set; }
    
    public static Event Create(
        string name,
        DateOnly start,
        DateOnly? end = null)
    {
        
        var obj = new Event();

        obj.RecordAuditEvent("system", $"Created new event {name} running from {start} to {end}");

        obj.Name = name;
        obj.Start = start;
        obj.End = end;
        
        return obj;
    }

    public void Update(        
        string name,
        DateOnly start,
        DateOnly? end = null)
    {
        RecordAuditEvent("system", "Updated ");
    }
    
}