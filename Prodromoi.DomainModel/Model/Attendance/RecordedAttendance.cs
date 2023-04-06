using Prodromoi.Core.Features;
using Prodromoi.DomainModel.Model.Members;

namespace Prodromoi.DomainModel.Model.Attendance;

public class RecordedAttendance : AuditEntity
{
    public int RecordingAdultId { get; private set; } = -1;
    public virtual Member RecordingAdult { get; private set; } = new();

    public virtual List<MemberAttendance> RecordedAttendances { get; private set; } = new();

    public DateTime Recorded { get; private set; } = new();

}