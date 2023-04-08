using Prodromoi.Core.Features;
using Prodromoi.DomainModel.Model.Members;

namespace Prodromoi.DomainModel.Model.Attendance;

public class SectionRecordedAttendance : AuditEntity
{
    public int RecordingAdultId { get; private set; } = 0;
    public DateTime Recorded { get; private set; } = DateTime.MinValue;

    public static SectionRecordedAttendance Create(Member member)
    {
        var obj = new SectionRecordedAttendance
        {
            RecordingAdultId = member.Id,
            Recorded = DateTime.Now.ToUniversalTime()
        };

        return obj;
    }
    
}