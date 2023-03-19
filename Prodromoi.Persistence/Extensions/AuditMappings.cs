using Microsoft.EntityFrameworkCore;
using Prodromoi.Core.Features;

namespace Prodromoi.Persistence.Extensions;

public static class AuditMappings
{
    public static IQueryable<T> AuditIncludes<T>(this IQueryable<T> audit) where T : AuditEntity
    {
        return audit
            .Include(ae => ae.AuditEntries);
    }
}