using Prodromoi.Dto.Members;

namespace Prodromoi.Dto.Attendance;

public class MemberAttendanceDto
{
    public bool Present { get; set; } = true;

    public MemberTinyDto Member { get; set; } = new();

}