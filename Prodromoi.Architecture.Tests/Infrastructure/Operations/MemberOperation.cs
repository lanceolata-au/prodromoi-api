using Autofac;
using Prodromoi.DomainModel.Model.Members;
using Prodromoi.Dto.Members;

namespace Prodromoi.Architecture.Tests.Infrastructure.Operations;

public class MemberOperation : Operation
{
    public MemberOperation(IContainer container) : base(container)
    {
    }

    public void TestMemberIsCreated()
    {
        var dto = new MemberDto()
        {

        };

        var member = Member.Create(dto);

        _readWriteRepository.Create<Member, int>(member);
        _readWriteRepository.Commit();

    }
    
}