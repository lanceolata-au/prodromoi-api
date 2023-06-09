using Prodromoi.Dto.Members;

namespace Prodromoi.Dto.Attendance;

public class QuickAttendanceDto
{
    public MemberTinyDto RecordingAdult { get; set; } = new();
    public List<MemberAttendanceDto> Attendances { get; set; }  = new();
}