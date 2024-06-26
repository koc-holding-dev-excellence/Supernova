using Microsoft.EntityFrameworkCore;
using Supernova.Domain;

namespace Supernova.Persistence.Context;

public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Product>(ApprovalMtvMapping.OnModelCreating);
        modelBuilder.Entity<Product>().ToTable("Product");

        base.OnModelCreating(modelBuilder);
    }
}
