using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prodromoi.Core.Features;
using Prodromoi.DomainModel.Model.Members;

namespace Prodromoi.DomainModel.Model.Attendance;

public class RecordedAttendance : Entity<bool>
{
    [NotMapped] 
    public override bool Id { get; protected set; }

    [Key]
    [Column(Order = 1)]
    public int SectionAttendanceId;
    
    [Key]
    [Column(Order = 2)]
    public int MemberId;

}