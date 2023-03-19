using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Prodromoi.Core.Features;

public class ProdromoiBaseDbContext : DbContext
{
    public ProdromoiBaseDbContext(DbContextOptions options) : base(options)
    {
    }

}