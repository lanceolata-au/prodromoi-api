using Microsoft.EntityFrameworkCore;

namespace Prodromoi.Service.Features.Data;

public class CoreContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}