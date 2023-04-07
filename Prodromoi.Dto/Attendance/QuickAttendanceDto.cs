using Prodromoi.Dto.Members;

namespace Prodromoi.Dto.Attendance;

public class QuickAttendanceDto
{
    public MemberDto RecordingAdult = new();
    public List<MemberAttendanceDto> Attendances = new();
}