using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prodromoi.Core.Dtos;
using Prodromoi.Core.Interfaces;

namespace Prodromoi.Core.Features;

public class AuditEntity : Entity<int>
{
    public virtual List<AuditEntry> AuditEntries { get; private set; } = new();

    [NotMapped] 
    public List<AuditDto> PendingAuditEntries { get; private set; } = new();

    protected AuditEntity(){}
        
    protected AuditEntity(int id)
    {
        if (Equals(id,default(int)))
        {
            throw new ArgumentException("The identifier cannot be default.", paramName: nameof(id));
        }

        // ReSharper disable once VirtualMemberCallInConstructor
        Id = id;
    }

    public override bool Equals(object? otherObject)
    {
        if (otherObject is Entity<int> entity && !Equals(Id, default(int)))
        {
            return Equals(entity);
        }
        // ReSharper disable once BaseObjectEqualsIsObjectEquals
        return base.Equals(otherObject);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    private bool Equals(Entity<int>? other)
    {
        return other != null && Id.Equals(other.Id);
    }

    public void Audit(string actor, string entry)
    {
        PendingAuditEntries.Add(new AuditDto()
        {
            Actor = actor,
            Entry = entry
        });
    }

}