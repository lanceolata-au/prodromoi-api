using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prodromoi.Core.Features;
using Prodromoi.DomainModel.Model.Members;

namespace Prodromoi.DomainModel.Model.Attendance;

public class RecordedAttendance : Entity<short>
{
    [NotMapped] 
    public override short Id { get; protected set; }

    [Key] [Column(Order = 1)] 
    public int SectionAttendanceId { get; private set; } = 0;
    public virtual SectionRecordedAttendance? SectionRecordedAttendance { get; private set; }
    
    [Key]
    [Column(Order = 2)]
    public int MemberId { get; private set; } = 0;
    public virtual Member? Member { get; private set; }

    public static RecordedAttendance Create(
        SectionRecordedAttendance sectionRecordedAttendance,
        Member member)
    {
        
        var obj = new RecordedAttendance
        {
            SectionAttendanceId = sectionRecordedAttendance.Id,
            MemberId = member.Id
        };

        return obj;
    }
    
}