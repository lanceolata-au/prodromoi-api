using System.Globalization;
using Prodromoi.Core.Features;
using Prodromoi.Core.Interfaces;
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
        Audit(string.Empty, $"Set regular meeting day to {dayOfWeek.ToString()}");
        RegularMeetingDay = dayOfWeek;
    }

    public void SetMeetingTime(TimeOnly time)
    {
        Audit(string.Empty, $"");
        RegularMeetingTime = time;

    }

    public void SetFriendlyCode(string friendlyCode)
    {
        Audit(string.Empty, $"Changed to have friendly code {friendlyCode}");
        FriendlyCode = friendlyCode.ToLower(CultureInfo.InvariantCulture);
    }
    
    public FormationSectionDto MapDto(IHashIdTranslator hashId)
    {
        return new FormationSectionDto()
        {
            HashId = hashId.Encode(Id),
            SectionType = SectionType,
            Formation = new FormationDto()
            {
                Name = Formation!.Name
            }
        };
    }
    
}