namespace Prodromoi.Dto.Members;

public class MemberDto : MemberTinyDto
{
    public int? RegistrationNumber { get; set; }
    public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
    public string? Email { get; set; }
}