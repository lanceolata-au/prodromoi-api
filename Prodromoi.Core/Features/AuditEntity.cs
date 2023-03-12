using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prodromoi.Core.Dtos;
using Prodromoi.Core.Interfaces;

namespace Prodromoi.Core.Features;

public class AuditEntity<TId> : IEntity<TId> where TId: struct
{
    [Key]
    public virtual TId Id { get; protected set; }
    
    public virtual List<AuditEntry> AuditEntries { get; internal set; } = new();

    [NotMapped] 
    public List<AuditDto> UpdateAuditEntries { get; private set; } = new();
    
    protected AuditEntity(){}
        
    protected AuditEntity(TId id)
    {
        if (Equals(id,default(TId)))
        {
            throw new ArgumentException("The identifier cannot be default.", paramName: nameof(id));
        }

        // ReSharper disable once VirtualMemberCallInConstructor
        Id = id;
    }

    public override bool Equals(object? otherObject)
    {
        if (otherObject is Entity<TId> entity && !Equals(Id, default(TId)))
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

    private bool Equals(Entity<TId>? other)
    {
        return other != null && Id.Equals(other.Id);
    }

    protected void RecordAuditEvent(
        string actor,
        string entry)
    {
        var sourceType = GetType().ToString();

        int? sourceId = null;
        
        if (Id is int) sourceId = Id as int?;

        UpdateAuditEntries.Add(new AuditDto()
        {
            Timestamp = DateTime.UtcNow,
            Actor = actor,
            Entry = entry,
            SourceId = sourceId,
            SourceType = GetType().ToString()
        });

    }
    
}