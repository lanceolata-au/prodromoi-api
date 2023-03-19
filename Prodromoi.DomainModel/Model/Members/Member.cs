using Prodromoi.Core.Features;

namespace Prodromoi.DomainModel.Model.Members;

public class Member : AuditEntity
{
    public string Name { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public MemberType MemberType { get; private set; } = MemberType.Unknown;

}