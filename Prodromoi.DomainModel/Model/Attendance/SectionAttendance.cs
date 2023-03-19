using Prodromoi.Core.Features;
using Prodromoi.DomainModel.Model.Members;

namespace Prodromoi.DomainModel.Model.Attendance;

public class SectionAttendance : AuditEntity
{
    public int RecordingAdultId { get; private set; }
    public virtual Member RecordingAdult { get; private set; }
    
    public DateTime Recorded { get; private set; }
    
}