using FluentAssertions;
using Prodromoi.Core.Features;
using Prodromoi.DomainModel.Model.Members;
using Prodromoi.Persistence.Extensions;

namespace Prodromoi.Architecture.Tests.Model;

public class AuditTests : TestWithDi
{
    [Test]
    public void CanCreateMemberWithAudit()
    {
        MemberOperation.TestMemberIsCreated();

        var members = ReadOnlyRepository
            .Table<Member, int>()
            .IncludeAudits();

        var audits = ReadOnlyRepository
            .Table<AuditEntry, long>();
        
        members.Count().Should().Be(1);

        audits.Count().Should().Be(1);

    }
}