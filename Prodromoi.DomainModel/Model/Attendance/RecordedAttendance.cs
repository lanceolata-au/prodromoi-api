using Prodromoi.Core.Features;
using Prodromoi.DomainModel.Model.Members;

namespace Prodromoi.DomainModel.Model.Attendance;

public class RecordedAttendance : Entity<long>
{
    public int SectionAttendanceId { get; private set; }
    public virtual SectionRecordedAttendance SectionAttendance { get; private set; }
    public int MemberId { get; private set; }
    public virtual Member Member { get; private set; }
}