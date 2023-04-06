using Prodromoi.Core.Features;

namespace Prodromoi.DomainModel.Model.Formations;

public class Formation : AuditEntity
{

    public string Name { get; private set; } = string.Empty;



    public virtual List<FormationSection> Sections { get; private set; } = new();
}