using Prodromoi.Core.Features;
using Prodromoi.DomainModel.Model.Formations;
using Prodromoi.DomainModel.Model.Members;

namespace Prodromoi.DomainModel.Model.Attendance;

public class SectionRecordedAttendance : AuditEntity
{
    public int RecordingAdultId { get; private set; } = 0;
    public virtual Member RecordingAdult { get; private set; } = null!;
    public int FormationSectionId { get; private set; } = 0;
    public virtual FormationSection FormationSection { get; private set; } = null!;
    public DateTime Recorded { get; private set; } = DateTime.MinValue;

    public static SectionRecordedAttendance Create(Member member, int formationSectionId)
    {
        var obj = new SectionRecordedAttendance
        {
            RecordingAdultId = member.Id,
            Recorded = DateTime.Now.ToUniversalTime(),
            FormationSectionId = formationSectionId
        };

        return obj;
    }
    
}