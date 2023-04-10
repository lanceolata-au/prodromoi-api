namespace Prodromoi.Dto.Formations;

public class FormationSectionDto
{
    public SectionType SectionType { get; set; } = SectionType.Undefined;
    public FormationDto Formation { get; set; } = new();
}