namespace Prodromoi.Dto.Formations;

public class FormationSectionDto
{
    public string HashId { get; set; } = string.Empty;
    public SectionType SectionType { get; set; } = SectionType.Undefined;
    public FormationDto Formation { get; set; } = new();
}