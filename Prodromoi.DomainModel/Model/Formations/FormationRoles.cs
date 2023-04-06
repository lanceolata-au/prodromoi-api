using Prodromoi.Core.Features;

namespace Prodromoi.DomainModel.Model.Formations;

public class FormationRoles : Entity<int>
{
    public string Name { get; private set; } = string.Empty;
}