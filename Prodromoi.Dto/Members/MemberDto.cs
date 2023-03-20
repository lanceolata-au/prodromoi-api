namespace Prodromoi.Dto.Members;

public class MemberDto
{
    public string Name { get; set; }
    public int? RegistrationNumber { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public MemberType MemberType { get; set; } = MemberType.Unknown;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}