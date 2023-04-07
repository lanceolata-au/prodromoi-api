using Prodromoi.Core.Extensions;
using Prodromoi.Core.Features;
using Prodromoi.Dto.Members;

namespace Prodromoi.DomainModel.Model.Members;

public class Member : AuditEntity
{
    public int? RegistrationNumber { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateOnly? DateOfBirth { get; private set; }
    public MemberType MemberType { get; private set; } = MemberType.Unknown;
    public string? PhoneNumber { get; private set; }
    public string? Email { get; private set; }

    public static Member Create(MemberDto dto)
    {
        var obj = new Member()
        {
            Name = dto.Name,
            RegistrationNumber = dto.RegistrationNumber,
            DateOfBirth = DateOnly.FromDateTime(dto.DateOfBirth),
            MemberType = dto.MemberType,
            PhoneNumber = dto.PhoneNumber.PhoneNumberString(),
            Email = dto.Email
        };

        obj.Audit(
            "System", 
            $"Created new member named {dto.Name}, of type {dto.MemberType}");
        
        return obj;
    }

}