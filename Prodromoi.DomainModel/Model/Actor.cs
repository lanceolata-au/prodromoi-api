using Prodromoi.Core.Features;

namespace Prodromoi.DomainModel.Model;

public class Actor : AuditEntity
{
    
    public string Name { get; private set; } = string.Empty;

    public static Actor Create(string name, Actor? actor = null)
    {
        var obj = new Actor
        {
            Name = name
        };

        obj.Audit("System", $"Created new Actor named {name}");
        
        return obj;
    }

    public void ChangeName(string name, Actor? actor = null)
    {
        Audit("System", $"Changed Actor named {Name} to {name}");
        Name = name;
    }
    
}