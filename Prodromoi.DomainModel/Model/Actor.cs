using Prodromoi.Core.Features;

namespace Prodromoi.DomainModel.Model;

public class Actor : AuditEntity<int>
{
    
    public string Name { get; private set; } = string.Empty;

    public static Actor Create(string name)
    {
        var obj = new Actor();

        obj.RecordAuditEvent("system", $"{name} was created as actor");
        
        obj.Name = name;

        return obj;
    }
}