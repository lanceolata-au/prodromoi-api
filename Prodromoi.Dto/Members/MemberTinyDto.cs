namespace Prodromoi.Dto.Members;

public class MemberTinyDto
{    
    public string Name { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public MemberType MemberType { get; set; } = MemberType.Unknown;
}