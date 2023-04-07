using Microsoft.EntityFrameworkCore;
using Prodromoi.DomainModel.Model.Attendance;

namespace Prodromoi.DomainModel.Inclusions;

public static class RecordedAttendanceIncludes
{
    public static IQueryable<RecordedAttendance> FullIncludes(this IQueryable<RecordedAttendance> project)
    {
        return project
            .Include(ra => ra.Member)
            .ThenInclude(mb => mb!.AuditEntries)
            .Include(ra => ra.SectionAttendance)
            .ThenInclude(sra => sra!.AuditEntries);
    }
}