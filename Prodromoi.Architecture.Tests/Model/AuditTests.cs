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
        _memberOperation.ThenTestMemberIsCreated();

        var members = _readOnlyRepository
            .Table<Member, int>()
            .AuditIncludes();

        var audits = _readOnlyRepository
            .Table<AuditEntry, long>();
        
        members.Count().Should().Be(1);

        audits.Count().Should().Be(1);

    }
}