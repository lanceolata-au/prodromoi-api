using Microsoft.EntityFrameworkCore;
using Prodromoi.Core.Features;
using Prodromoi.DomainModel.Model.Events;

namespace Prodromoi.Service.Extensions.Mappings;

public static class AuditMappings
{
    public static IQueryable<AuditEntity<long>> AuditIncludes(this IQueryable<AuditEntity<long>> audit)
    {
        return audit
            .Include(ae => ae.AuditEntries);
    }
}