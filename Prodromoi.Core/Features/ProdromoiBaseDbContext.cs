using Microsoft.EntityFrameworkCore;

namespace Prodromoi.Core.Features;

public class ProdromoiBaseDbContext : DbContext
{
    public ProdromoiBaseDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    private DbSet<AuditEntry> AuditEntries { get; set; }

}