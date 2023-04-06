using Prodromoi.Core.Features;
using Prodromoi.DomainModel.Model.Members;

namespace Prodromoi.DomainModel.Model.Formations;

public class FormationMembership : AuditEntity
{
    public int FormationId { get; private set; } = -1;

    public int MemberId { get; private set; } = -1;
    public virtual Member Member { get; private set; } = new();

}