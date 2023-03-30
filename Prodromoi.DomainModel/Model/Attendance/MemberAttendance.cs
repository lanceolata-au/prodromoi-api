using Prodromoi.Core.Features;
using Prodromoi.DomainModel.Model.Members;

namespace Prodromoi.DomainModel.Model.Attendance;

public class MemberAttendance : Entity<long>
{
    public int SectionAttendanceId { get; private set; }
    public virtual RecordedAttendance SectionAttendance { get; private set; }
    public int MemberId { get; private set; }
    public virtual Member Member { get; private set; }
}