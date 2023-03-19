using Microsoft.EntityFrameworkCore;
using Prodromoi.Core.Features;

namespace Prodromoi.Persistence.Extensions;

public static class AuditMappings
{
    public static IQueryable<T> AuditIncludes<T, TId>(this IQueryable<T> audit) where T : AuditEntity<TId> where TId : struct
    {
        return audit
            .Include(ae => ae.AuditEntries);
    }
}