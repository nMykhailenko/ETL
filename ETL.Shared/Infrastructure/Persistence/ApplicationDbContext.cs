using ETL.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETL.Shared.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)        
    {        
    }

    public DbSet<HashEntity> Hashes { get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
    }
}