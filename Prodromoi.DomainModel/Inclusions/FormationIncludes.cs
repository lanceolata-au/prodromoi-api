using Microsoft.EntityFrameworkCore;
using Prodromoi.DomainModel.Model.Formations;

namespace Prodromoi.DomainModel.Inclusions;

public static class FormationIncludes
{
    public static IQueryable<Formation> FullIncludes(this IQueryable<Formation> formation)
    {
        return formation
            .Include(fm => fm.Sections)
            .ThenInclude(mb => mb!.AuditEntries)
            .Include(ra => ra.AuditEntries);
    }
    
    public static IQueryable<FormationSection> BasicIncludes(this IQueryable<FormationSection> formationSection)
    {
        return formationSection
            .Include(fm => fm.Formation);

    }
    
}