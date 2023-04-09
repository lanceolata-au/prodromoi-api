using Prodromoi.Core.Features;
using Prodromoi.Dto.Formations;

namespace Prodromoi.DomainModel.Model.Formations;

public class FormationSection : AuditEntity
{
    public SectionType SectionType { get; set; } = SectionType.Undefined;
    public int FormationId { get; set; } = 0;
    public virtual Formation? Formation { get; set; }

    public DayOfWeek? RegularMeetingDay { get; set; }
    public TimeOnly? RegularMeetingTime { get; set; }
    public string? FriendlyCode { get; set; }

    protected FormationSection() { }

    public static FormationSection Create(
        int formationId, 
        SectionType sectionType)
    {
        var obj = new FormationSection
        {
            FormationId = formationId,
            SectionType = sectionType
        };
        
        return obj;
    }

    public void SetMeetingDay(DayOfWeek dayOfWeek)
    {
        Audit(string.Empty, $"Set meeting day to {dayOfWeek.ToString()}");
        RegularMeetingDay = dayOfWeek;
    }
    
}