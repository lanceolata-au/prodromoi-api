namespace Prodromoi.Dto.Members;

public class MemberDto : MemberTinyDto
{
    public string Name { get; set; } = string.Empty;
    public int? RegistrationNumber { get; set; }
    public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}